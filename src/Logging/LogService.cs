using System;
using System.Collections.Generic;

namespace TestFramework.Logging
{
    /// <summary>
    /// Сервис логгирования, осуществляет управление лог сессиями
    /// </summary>
    public interface ILogService : ILog, IDisposable
    {
        void Start();
        ILogSession NewSession(string title, LogLevel level, string type = null);
        ILogSession CurrentSession { get; }
    }

    /// <summary>
    /// Сервис логгирования, осуществляет управление лог сессиями
    /// </summary>
    public class LogService : ILogService
    {
        private Stack<ILogSession> sessions = new Stack<ILogSession>();

        private event Action<ILogSession> SessionPushed = delegate { };
        private event Action<ILogSession> SessionPopped = delegate { };
        private event Action ServiceStarting = delegate { };
        private event Action ServiceStopped = delegate { };
        private long sessionId = 1;

        public LogService(IEnumerable<ILogProvider> providers)
        {
            foreach(var provider in providers)
            {
                ServiceStarting += provider.OnServiceStart;

                SessionPushed += provider.OnCreateSession;
                SessionPushed += session => session.MessageSending += provider.OnMessage;

                SessionPopped += session => session.MessageSending -= provider.OnMessage;
                SessionPopped += provider.OnDisposeSession;

                ServiceStopped += provider.OnServiceStop;
            }
            Start();
        }

        public void Start()
        {
            ServiceStarting();
            NewSession("Запуск теста", LogLevel.Info, "");
        }

        public ILogSession CurrentSession => sessions.Count != 0 ? sessions.Peek() : throw new Exception("У лог-сервиса не создано лог-сессий, возможно лог сервис не был запущен");

        private void PushSession(ILogSession session)
        {
            sessions.Push(session);
            SessionPushed(session);
        }

        private void PopSession(ILogSession session)
        {
            var currentSession = CurrentSession;
            if (currentSession != session) throw new Exception($"Невозможно завержить лог сессию \"{session.Title}\", сначала требуется завершить сессию \"{currentSession.Title}\"");
            var poppedSession = sessions.Pop();
            SessionPopped(poppedSession);
        }

        public ILogSession NewSession(string title, LogLevel level, string type)
        {
            var session = new LogSession(sessionId, DateTime.Now, title, LogLevel.Info, type, sessions.Count != 0 ? sessions.Peek() : null);
            sessionId++;
            session.Disposing += PopSession;
            PushSession(session);
            return session;
        }


        public void Trace(object message, params IAttachment[] attachments) => CurrentSession.Trace(message, attachments);

        public void Debug(object message, params IAttachment[] attachments) => CurrentSession.Debug(message, attachments);

        public void Info(object message, params IAttachment[] attachments) => CurrentSession.Info(message, attachments);

        public void Warning(object message, params IAttachment[] attachments) => CurrentSession.Warning(message, attachments);

        public void Error(object message, params IAttachment[] attachments) => CurrentSession.Error(message, attachments);

        public void Dispose()
        {
            while (sessions.Count > 0)
            {
                var session = sessions.Pop();
                SessionPopped(session);
            }
            ServiceStopped();
        }
    }
}

using System;
using System.Collections.Generic;

namespace TestFramework.Logging
{
    public interface ILog
    {
        void Trace(object message, params IAttachment[] attachments);
        void Debug(object message, params IAttachment[] attachments);
        void Info(object message, params IAttachment[] attachments);
        void Warning(object message, params IAttachment[] attachments);
        void Error(object message, params IAttachment[] attachments);
    }

    public interface ILogSession : ILog, IDisposable
    {
        long Id { get; }
        string Title { get; }
        string SessionType { get; }
        LogLevel Level { get; }
        DateTime Date { get; }
        event Action<ILogSession> Disposing;
        event Action<ILogMessage> MessageSending;
        ILogSession Parrent { get; }
    }

    /// <summary>
    /// Лог-сессия, все сообщения в логе пишутся в лог сессии
    /// </summary>
    public class LogSession : ILogSession
    {
        private readonly List<ILogMessage> messages = new List<ILogMessage>();

        public LogSession(long id, DateTime date, string title, LogLevel level, string sessionType, ILogSession parrent)
        {
            Id = id;
            Date = date;
            Title = title;
            Level = level;
            SessionType = sessionType;
            Parrent = parrent;
        }

        public event Action<ILogSession> Disposing = delegate { };
        public event Action<ILogMessage> MessageSending = delegate { };
        public string Title { get; }
        public long Id { get; }
        public DateTime Date { get; }
        public LogLevel Level { get; }
        public string SessionType { get; }
        public ILogSession Parrent { get; }

        public void Trace(object message, params IAttachment[] attachments)
        {
            MessageSending(new LogMessage(this, DateTime.Now, message.ToString(), LogLevel.Trace, attachments));
        }

        public void Debug(object message, params IAttachment[] attachments)
        {
            MessageSending(new LogMessage(this, DateTime.Now, message.ToString(), LogLevel.Debug, attachments));
        }

        public void Info(object message, params IAttachment[] attachments)
        {
            MessageSending(new LogMessage(this, DateTime.Now, message.ToString(), LogLevel.Info, attachments));
        }

        public void Warning(object message, params IAttachment[] attachments)
        {
            MessageSending(new LogMessage(this, DateTime.Now, message.ToString(), LogLevel.Warning, attachments));
        }

        public void Error(object message, params IAttachment[] attachments)
        {
            MessageSending(new LogMessage(this, DateTime.Now, message.ToString(), LogLevel.Error, attachments));
        }

        public void Dispose()
        {
            Disposing(this);
        }
    }
}

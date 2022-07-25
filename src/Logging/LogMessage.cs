using System;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.Logging
{
    public enum LogLevel
    {
        Trace = 0,
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }

    public interface ILogMessage
    {
        string Text { get; }
        DateTime Date { get; }
        LogLevel Level { get; }
        ILogSession Session { get; }
        ICollection<IAttachment> Attachments { get; }
    }

    public class LogMessage : ILogMessage
    {
        public LogMessage(ILogSession session, DateTime date, string text, LogLevel level, IAttachment[] attachments) 
        {
            Session = session;
            Date = date;
            Text = text;
            Level = level;
            if(attachments != null)
                Attachments = attachments.ToList();
        }

        public ILogSession Session { get; }
        public DateTime Date { get; }
        public string Text { get; }
        public LogLevel Level { get; }

        public ICollection<IAttachment> Attachments { get; } = new List<IAttachment>();
    }
}

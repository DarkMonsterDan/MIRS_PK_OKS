using System;
using System.Collections.Generic;
using System.Text;

namespace TestFramework.Logging
{
    /// <summary>
    /// Аргументы для события, возникающего при уничтожении лог сессии
    /// </summary>
    public class LogSessionDisposeEventArgs : EventArgs
    {
        public LogSessionDisposeEventArgs(ILogSession disposingSession, ILogSession parentSession)
        {
            DisposingSession = disposingSession;
            ParentSession = parentSession;
        }

        /// <summary>
        /// Родительская, отностительно уничтожаемой, лог сессия
        /// </summary>
        public ILogSession ParentSession { get; private set; }

        /// <summary>
        /// Уничтажаемая лог сессия
        /// </summary>
        public ILogSession DisposingSession { get; private set; }
    }
}

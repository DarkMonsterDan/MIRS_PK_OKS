using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestFramework.IO;

namespace TestFramework.Logging
{
    public interface ILogProvider
    {
        void OnServiceStart();
        void OnCreateSession(ILogSession session);
        void OnDisposeSession(ILogSession session);
        void OnMessage(ILogMessage message);
        void OnServiceStop();
    }

    public class FileLogProviderSetings
    {
        public LogLevel MinLogLevel { get; set; }
        public string FileName { get; set; }
    }

    /// <summary>
    /// Провайдер для записи логов в файл
    /// </summary>
    public class FileLogProvider : ILogProvider
    {
        const string datePattern = "HH:mm:ss";
        const string style = @"
body{
    font-family: monospace;
    font-size: 14;
}
ul{
    display: block;
    list-style: none; padding-left:1.5em;
}

ul ul{
    border-left: 1px dotted;
}

input {
    display: none;
    
}

input ~ label{
    coursor: pointer;
}

input:checked ~ ul {
    display: none;
}

input:checked ~ .session-closed {
    display: none;
}

input:checked ~ label span {
    display: none;
}

.error {
    background-color: #FFCCCC;
}";

        public FileLogProvider(IFileService fileService, FileLogProviderSetings settings)
        {
            this.fileService = fileService;
            this.settings = settings;
        }

        private TextFile file;
        private readonly IFileService fileService;
        private readonly FileLogProviderSetings settings;

        public void OnCreateSession(ILogSession session)
        {
            var id = Guid.NewGuid();
            file.AppendLine($"<li class='{session.SessionType} session-title'><input type='checkbox' id='{id}' /><label for='{id}'>{session.Date.ToString(datePattern)} <b><span class='session-opened'>Начало: </span>{session.Title.Replace(Environment.NewLine, $"</br>")}</b></label>");
            file.AppendLine($"<ul class='{session.SessionType}'>");
        }

        public void OnDisposeSession(ILogSession session)
        {
            file.AppendLine($"</ul><span class='session-closed'><span>{DateTime.Now.ToString(datePattern)} </span><span><b>Завершение: {session.Title.Replace(Environment.NewLine, $"</br>")}</b></span></span></li>");
        }

        public void OnMessage(ILogMessage message)
        {
            var fileAttachments = message.Attachments.Where(x => x.Type == "File");
            var screenshots = fileAttachments.Where(x => x.Title == "Screenshot");
            var anotherFileAttachments = fileAttachments.Except(screenshots);
            var screenshotsText = string.Join(" ", screenshots.Select(x => $"<a href='{x.Content}'>{x.Title}</a>"));
            var anotherFileAttachmentsText = string.Join(" ", anotherFileAttachments.Select(x => $"<a href='{x.Content}'>{x.Title}</a>"));
            var text = $"{message.Date.ToString(datePattern)} {screenshotsText} {message.Text.Replace(Environment.NewLine, "</br>")}{anotherFileAttachmentsText}";
            text = $"<li class='{message.Level.ToString().ToLower()}'>{text}</li>";
            file.AppendLine(text);
        }

        public void OnServiceStart()
        {
            file = new TextFile(fileService.CreateFile(settings.FileName));
            file.AppendLine("<html>");
            file.AppendLine($"<head><meta charset=\"UTF-8\"><style>{style}</style></head>");
            file.AppendLine("<body><ul>");
        }

        public void OnServiceStop()
        {
            file.AppendLine("</ul></body>");
            file.AppendLine("</html>");
        }
    }
}

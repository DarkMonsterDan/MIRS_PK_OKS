using System;
using TestFramework.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LogExtensions
    {
        public static void AddLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILogService, LogService>();
        }

        public static void Trace(this ILogService logService, object message, params IAttachment[] attachments) => logService.CurrentSession.Trace(message, attachments);

        public static void Debug(this ILogService logService, object message, params IAttachment[] attachments) => logService.CurrentSession.Debug(message, attachments);

        public static void Info(this ILogService logService, object message, params IAttachment[] attachments) => logService.CurrentSession.Info(message, attachments);

        public static void Warning(this ILogService logService, object message, params IAttachment[] attachments) => logService.CurrentSession.Warning(message, attachments);

        public static void Error(this ILogService logService, object message, params IAttachment[] attachments) => logService.CurrentSession.Error(message, attachments);
    }
}

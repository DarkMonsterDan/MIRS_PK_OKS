using TestFramework.Logging;
using System;
using TestFramework.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileLogProviderExtensions
    {
        public static void AddFileLogProvider(this IServiceCollection services, FileLogProviderSetings settings)
        {
            services.AddTransient<ILogProvider>(x => new FileLogProvider(x.GetRequiredService<IFileService>(), settings));
        }

        public static void AddFileLogProvider(this IServiceCollection services, Action<FileLogProviderSetings> configure)
        {
            var settings = new FileLogProviderSetings();
            configure(settings);
            AddFileLogProvider(services, settings);
        }
    }
}

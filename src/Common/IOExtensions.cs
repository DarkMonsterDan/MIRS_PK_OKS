using Microsoft.Extensions.DependencyInjection;
using TestFramework.IO;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IOExtensions
    {
        public static void AddIO(this IServiceCollection services, IOSettings settings)
        {
            services.AddSingleton<IFileService>(new FileService(settings.OutputPath));
            services.AddSingleton(settings);
        }

        public static void AddIO(this IServiceCollection services, Action<IOSettings> changeSettings)
        {
            var settings = new IOSettings();
            changeSettings(settings);
            AddIO(services, settings);
        }

    }
}

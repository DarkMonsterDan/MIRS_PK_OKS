using Refit;
using TestFramework.Rest;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RestExtensions
    {
        public static void AddRestApi<T>(this IServiceCollection services, string url, Action<RefitSettings> settings = null)
            where T : class
        {
            services.AddSingleton<HttpMessageHandler, HttpClientHandler>();
            services.AddSingleton<LoggingMessageHandler>();
            services.AddSingleton(sp =>
            {
                var s = new RefitSettings
                {
                    HttpMessageHandlerFactory = () => sp.GetRequiredService<LoggingMessageHandler>()
                };
                settings?.Invoke(s);
                return RestService.For<T>(url, s);
            });

        }
    }
}

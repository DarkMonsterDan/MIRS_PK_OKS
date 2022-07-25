using TestFramework.Core;
using TestFramework.UI;
using TestFramework.UI.Parsers;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UIExtensions
    {
        public static IServiceCollection AddUI(this IServiceCollection services)
        {
            var assembly = typeof(UIComponent).Assembly;
            services.AddComponents(assembly);
            services.AddTransient<IParser<string>, StringParser>();
            services.AddTransient<IParser<int>, IntParser>();
            services.AddTransient<IParser<double>, DoubleParser>();
            services.AddTransient<IParser<decimal>, DecimalParser>();
            services.AddTransient<IParser<DateTime>, DateTimeParser>();
            services.AddTransient<IParser<TimeSpan>, TimeSpanParser>();
            services.AddSingleton<IApplicationPool, ApplicationPool>();
            return services;
        }
    }


}

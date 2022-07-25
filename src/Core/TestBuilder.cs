using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestFramework.Logging;
using System;

namespace TestFramework.Core
{
    public interface ITestBuilder
    {
        /// <summary>
        /// Инициализирует компоненты теста
        /// </summary>
        /// <param name="test">экземпляр теста</param>
        /// <returns>Контекст утравления тестом</returns>
        IFrameworkContext Build(object test);

        /// <summary>
        /// Регистрирует зависимости теста в DI контейнере
        /// </summary>
        ITestBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Инициализирует конфигурацию теста
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        ITestBuilder ConfigureTest(Action<IConfigurationBuilder> configure);
    }

    public class TestBuilder<TBootstrapper> : ITestBuilder where TBootstrapper : class, IBootstrapper
    {
        private readonly IServiceCollection services = new ServiceCollection();
        private Action<IConfigurationBuilder> configureTest = delegate { };

        public TestBuilder()
        {
            services.AddSingleton<IBootstrapper, TBootstrapper>();
            services.AddSingleton<IComponentInitializer, ComponentInitializer>();
            services.AddSingleton<ITestEnvironment, TestEnvironment>();
            services.AddSingleton<IAssert, AssertService>();
            services.AddSingleton(typeof(IFactory<>), typeof(Factory<>));
        }

        public IFrameworkContext Build(object test)
        {
            var context = new TestFrameworkContext(test);
            services.AddSingleton<IFrameworkContext>(context);
            var configuretionBuilder = new ConfigurationBuilder();
            configureTest(configuretionBuilder);
            services.AddSingleton<IConfiguration>(configuretionBuilder.Build());

            var provider = services.BuildServiceProvider();

            var bootstraper = provider.GetRequiredService<IBootstrapper>();
            bootstraper.ConfigureServices(services);

            provider = services.BuildServiceProvider();

            var componentInitializer = provider.GetService<IComponentInitializer>();
            bootstraper.Configure(componentInitializer);
            componentInitializer.Initialize(test);

            context.TestStopped += provider.Dispose;
            return context;
        }

        public ITestBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            configureServices?.Invoke(services);
            return this;
        }

        public ITestBuilder ConfigureTest(Action<IConfigurationBuilder> configure)
        {
            configureTest += configure;
            return this;
        }
    }

    public interface ITestInfo
    {
        /// <summary>
        /// Тест запущен
        /// </summary>
        event Action TestStarted;

        /// <summary>
        /// Тест остановлен
        /// </summary>
        event Action TestStopped;

        /// <summary>
        /// Экземпляр текущего теста
        /// </summary>
        object Test { get; }

        string TestId { get; }

        string TestCategory { get; }

        string Description { get; }

        TimeSpan Timeout { get; }
    }



    public interface ITestEnvironment
    {
        string RootPath { get; }
        string UserName { get; }
        string FullUserName { get; }

    }

    public class TestEnvironment : ITestEnvironment
    {
        public string RootPath => Environment.CurrentDirectory;

        public string UserName => Environment.UserName;

        public string FullUserName => $"{Environment.UserDomainName}/{UserName}";
    }
}

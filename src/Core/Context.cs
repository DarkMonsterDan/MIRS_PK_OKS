using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace TestFramework.Core
{
    /// <summary>
    /// Контекст управления выполнения теста
    /// </summary>
    public interface IFrameworkContext
    {
        /// <summary>
        /// Запуск теста
        /// </summary>
        IFrameworkContext Start();

        /// <summary>
        /// Остановка теста
        /// </summary>
        IFrameworkContext Stop();

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
    }

    public class TestFrameworkContext : IFrameworkContext
    {

        public TestFrameworkContext(object test)
        {
            Test = test;
        }
 
        public object Test { get; }
        public event Action TestStarted = delegate { };
        public event Action TestStopped = delegate { };

        public IFrameworkContext Start()
        {
            TestStarted();
            return this;
        }
        public IFrameworkContext Stop()
        {
            TestStopped();
            return this;
        }
    }
}

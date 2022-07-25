using Microsoft.Extensions.DependencyInjection;
using System;

namespace TestFramework.Core
{
    /// <summary>
    /// Управляет зависимостями и инициализацией теста
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Конфигурирование инициализации компонентов
        /// </summary>
        /// <param name="components">Инициализатор компонент</param>
        void Configure(IComponentInitializer components);

        /// <summary>
        /// Регистрирует типы в DI контейнере
        /// </summary>
        /// <param name="services">Коллекция описаний дескрипторов типов</param>
        void ConfigureServices(IServiceCollection services);
    }
}

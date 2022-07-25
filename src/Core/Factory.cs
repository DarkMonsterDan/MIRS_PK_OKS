using System;
using Microsoft.Extensions.DependencyInjection;

namespace TestFramework.Core
{
    /// <summary>
    /// Фабрика для создания компонент заданного типа
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface IFactory<TComponent> where TComponent : IComponent
    {
        /// <summary>
        /// Создает проинициализированную компоненту <typeparamref name="TComponent"/>
        /// </summary>
        TComponent Create();

        /// <summary>
        /// Создает проинициализированную компоненту <setypeparamref name="TComponent"/>
        /// </summary>
        TComponent Create(object parent);
    }

    public class Factory<TComponent> : IFactory<TComponent> where TComponent : IComponent
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IComponentInitializer componentInitializer;

        public Factory(IServiceProvider serviceProvider, IComponentInitializer componentInitializer)
        {
            this.serviceProvider = serviceProvider;
            this.componentInitializer = componentInitializer;
        }

        public TComponent Create()
        {
            var component = serviceProvider.GetService<TComponent>();
            componentInitializer.Initialize(component);
            return component;
        }

        public TComponent Create(object parent)
        {
            var component = serviceProvider.GetService<TComponent>();
            componentInitializer.Initialize(parent, component);
            return component;
        }
    }
}

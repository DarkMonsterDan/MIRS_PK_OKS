using Microsoft.Extensions.DependencyInjection;
using TestFramework.Core.Exceptions;
using TestFramework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace TestFramework.Core
{
    public static class CoreExtensions
    {
        /// <summary>
        /// Регистрирует все компоненты в сборке
        /// </summary>
        public static IServiceCollection AddComponents(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => typeof(IComponent).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract);
            foreach (var type in types)
            {
                services.AddTransient(type);
            }
            return services;
        }

        /// <summary>
        /// Регистрирует компонент
        /// </summary>
        public static IServiceCollection AddComponent<TComponent>(this IServiceCollection services)
            where TComponent : class, IComponent
        {
            services.AddTransient<TComponent>();
            return services;
        }

        /// <summary>
        /// Регистрирует компонент
        /// </summary>
        public static IServiceCollection AddComponent<TComponent, TImplementation>(this IServiceCollection services)
            where TComponent : class, IComponent
            where TImplementation : class, TComponent
        {
            services.AddTransient<TComponent, TImplementation>();
            return services;
        }

        /// <summary>
        /// Регистрирует компонент
        /// </summary>
        public static IServiceCollection AddComponent<TComponent>(this IServiceCollection services, Func<IServiceProvider, TComponent> implementationFactory)
            where TComponent : class, IComponent
        {
            services.AddTransient(implementationFactory);
            return services;
        }


        /// <summary>
        /// Выполняет <see cref="action"/> над объектом, позволяет создать короткий временный псевдоним для обработки объекта
        /// </summary>
        /// <typeparam name="T">Тип текущего объекта</typeparam>
        /// <param name="object">Объект для обработки</param>
        /// <param name="action">Функция обработки</param>
        /// <returns>Обработанный объект</returns>
        /// <example> 
        /// SuperDuperLongNamedPerson.With(x => 
        /// {
        ///     x.FirstName = "Иван";
        ///     x.LastName = "Иванов";
        ///     x.MiddleName = "Иванович";
        /// });
        /// </example> 
        public static T With<T>(this T @object, Action<T> action)
        {
            action?.Invoke(@object);
            return @object;
        }

        /// <summary>
        /// Возвращает корневой элемент дерева компонент
        /// </summary>
        /// <param name="metaInfo"></param>
        /// <returns></returns>
        public static object GetRoot(this IMetaInfo metaInfo) => GetBranch(metaInfo).Last();


        /// <summary>
        /// Возвращает последовательность элеметов от родителя текущего компонента до корневого узла дерева компонент 
        /// </summary>
        /// <param name="metaInfo"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetBranch(this IMetaInfo metaInfo)
        {
            //TODO: вечный цикл
            var currentMetaInfo = metaInfo;
            while (currentMetaInfo != null)
            {
                var parent = currentMetaInfo.Parent;
                if (parent != null)
                    yield return parent;
                currentMetaInfo = (parent as IComponent)?.MetaInfo;
            }
        }
    }
}

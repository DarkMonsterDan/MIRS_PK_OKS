using Microsoft.Extensions.DependencyInjection;
using TestFramework.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestFramework.Core
{
    /// <summary>
    /// Инициализатор компонентов
    /// </summary>
    public interface IComponentInitializer
    {
        /// <summary>
        /// Рекурсивно инициализирует все дочерние компоненты
        /// </summary>
        /// <param name="current">Текущий объект, содержащий компоненты, объявленные как свойства</param>
        void Initialize(object current);

        /// <summary>
        /// Рекурсивно инициализирует все дочерние компоненты и устанавливает компоненте parent 
        /// </summary>
        /// <param name="current">Текущий объект содержащий компоненты, объявленные как свойства</param>
        /// <param name="parent">Будет установлен текущему объекту как родитель, если текущий объект <see cref="IComponent"/></param>
        void Initialize(object parent, object current);

        /// <summary>
        /// Компонент был проинициализирован
        /// </summary>
        event Action<ComponentInitializingEventArgs> ComponentInitializing;

        /// <summary>
        /// Сервис-провайдер для получения экземпляров из DI контейнера
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }

    public class ComponentInitializer : IComponentInitializer
    {
        public ComponentInitializer(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public event Action<ComponentInitializingEventArgs> ComponentInitializing = delegate { };

        public void Initialize(object current) => Initialize(null, current);

        public IEnumerable<MemberInfo> GetImportableMembers(Type type)
        {
            var members = new List<MemberInfo>();

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => (field.GetCustomAttribute<ImportAttribute>(true) != null && !field.IsPrivate)
                    || (field.FieldType.GetCustomAttribute<ImportableAttribute>(true) != null && field.IsPublic));
            members.AddRange(fields);

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetCustomAttribute<ImportAttribute>(true) != null 
                    || x.PropertyType.GetCustomAttribute<ImportableAttribute>(true) != null);

            members.AddRange(properties);
            return members;
        }     

        public void Initialize(object parent, object current)
        {
            if (current == null)
                throw new ArgumentNullException(nameof(current));
            var type = current.GetType();
            var metaInfo = new MetaInfo();

            var children = new List<IComponent>();

            foreach (var member in GetImportableMembers(type))
            {
                var value = InitializeMember(member);

                if (value is IComponent child)
                {
                    child.MetaInfo.Name = member.Name;
                    foreach (var attribute in member.GetCustomAttributes(true))
                        child.MetaInfo.MemberAttributes.Add(attribute);
                    children.Add(child);
                    Initialize(current, child);
                }

                switch (member)
                {
                    case PropertyInfo property:
                        property.SetValue(current, value);
                        break;
                    case FieldInfo field:
                        field.SetValue(current, value);
                        break;
                }
            }

            if (current is IComponent component)
            {
                component.MetaInfo.Parent = parent;
                foreach (var child in children)
                    component.MetaInfo.Children.Add(child);
                foreach (var attribute in type.GetCustomAttributes(true))
                    component.MetaInfo.TypeAttributes.Add(attribute);
                component.Initialize();
            }
        }

        object InitializeMember(MemberInfo member)
        {
            var type = (member as PropertyInfo)?.PropertyType ?? (member as FieldInfo).FieldType;
            var members = GetImportableMembers(type);

            var dictionary = new Dictionary<MemberInfo, object>();
            foreach (var m in members)
            {
                dictionary.Add(m, InitializeMember(m));
            }

            var optional = member.GetCustomAttribute<OptionalAttribute>(true) != null;
            var value = optional
                ? ServiceProvider.GetService(type)
                : ServiceProvider.GetRequiredService(type);

            foreach(var m in members)
            {
                switch(m)
                {
                    case PropertyInfo property: property.SetValue(value, dictionary[m]);
                            break;
                    case FieldInfo field: field.SetValue(value, dictionary[m]);
                        break;
                }
            }

            return value;

        }
    }
}

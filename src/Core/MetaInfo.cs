using TestFramework.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace TestFramework.Core
{
    /// <summary>
    /// Мета информация, требуемая для инициализации компонента
    /// </summary>
    public interface IMetaInfo
    {
        /// <summary>
        /// Родитеь текущего компонента
        /// </summary>
        object Parent { get; set; }

        /// <summary>
        /// Возвращает атрибут компонента заданного типа
        /// Атрибут установленный для поля переопределит атрибут установленный для всего типа заданного компонента
        /// </summary>
        /// <typeparam name="TAttribute">Тип запрашиваемого атрибута</typeparam>
        /// <param name="required">Если true и если атрибут заданного типа не найден будет выброшено исключение</param>
        /// <exception cref="RequiredAttributeNotFoundException">Обязательный для компонента атрибут не найден</exception>
        /// <returns></returns>
        TAttribute GetRequiredAttribute<TAttribute>() where TAttribute : class;


        TAttribute GetAttribute<TAttribute>() where TAttribute : class;

        /// <summary>
        /// Имя свойства, которым был объявлен компонент
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Атрибуты, устрановленные для типа компонента 
        /// </summary>
        ICollection<object> TypeAttributes { get; }

        /// <summary>
        /// Атрибуты установленные при объявлении компонента как члена другого класса
        /// </summary>
        ICollection<object> MemberAttributes { get; }

        /// <summary>
        /// Компоненты объявленные как члены текущего компонента
        /// </summary>
        ICollection<IComponent> Children { get; }
    }

    public class MetaInfo : IMetaInfo
    {
        public string Name { get; set; }
        public object Parent { get; set; }
        public ICollection<IComponent> Children { get; } = new List<IComponent>();
        public ICollection<object> TypeAttributes { get; } = new List<object>();
        public ICollection<object> MemberAttributes { get; } = new List<object>();

        public TAttribute GetRequiredAttribute<TAttribute>() where TAttribute : class
        {
            return GetAttribute<TAttribute>() 
                ?? throw new RequiredAttributeNotFoundException($"У компонента \"{Name}\" не обнаружен обязательный атрибут \"{typeof(TAttribute).Name}\"");
        }

        public TAttribute GetAttribute<TAttribute>() where TAttribute : class
        {
            return MemberAttributes.OfType<TAttribute>()
            .LastOrDefault() ?? TypeAttributes
                .OfType<TAttribute>()
                .LastOrDefault();
        }
    }
}

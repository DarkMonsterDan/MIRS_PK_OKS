using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestFramework.Core
{
    /// <summary>
    /// Коллекция компонент
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComponentCollection<T> : IEnumerable<T>
    {
        /// <summary>
        /// Фильтрует заданную последовательность по заданному предикату
        /// </summary>
        /// <param name="predicate">Функция фильтрации</param>
        IComponentCollection<T> Where(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Перефодит каждый еслемент последовательности в новую форму
        /// </summary>
        /// <typeparam name="TResult">Новый тип элементов последовательности</typeparam>
        /// <param name="selector">Функция перевода в новый тип</param>
        IComponentCollection<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Возвращает заданное количество элементов начиная с начала последовательности
        /// </summary>
        /// <param name="count">Количество возвращаемых элементов последовательности</param>
        IComponentCollection<T> Take(int count);

        /// <summary>
        /// Пропускает заданное количество элементов последовательности и возвращает все остальные
        /// </summary>
        /// <param name="count">Количество пропускаемых элементов последовательнсти</param>
        IComponentCollection<T> Skip(int count);
    }

    public class ComponentCollection<T> : IComponentCollection<T>
    {

        private readonly IEnumerable<T> components;
        private readonly ExpressionDescriptionGeneratorHelper expressionHelper = new ExpressionDescriptionGeneratorHelper();

        public ComponentCollection(IEnumerable<T> components, string description)
        {
            this.components = components;
            Description = description;
        }

        public string Description { get; }

        public IEnumerator<T> GetEnumerator() => components.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)components).GetEnumerator();

        public IComponentCollection<T> Where(Expression<Func<T, bool>> predicate)
        {
            var predicateDelegate = predicate.Compile();
            var expressionDescription = expressionHelper.GetDescription(predicate);
            return new ComponentCollection<T>(components.Where(predicateDelegate), $"{this}{Environment.NewLine}  Где: {expressionDescription}");
        }

        public IComponentCollection<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        {
            var selectorDelegate = selector.Compile();
            return new ComponentCollection<TResult>(components.Select(selectorDelegate), Description);
        }

        public IComponentCollection<T> Take(int count)
        {
            return new ComponentCollection<T>(components.Take(count), $"{this}{Environment.NewLine}  Взять: {count}");
        }

        public IComponentCollection<T> Skip(int count)
        {
            return new ComponentCollection<T>(components.Skip(count), $"{this}{Environment.NewLine}  Пропустить: {count}");
        }
    }
}

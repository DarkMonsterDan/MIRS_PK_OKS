using TestFramework.Core.Exceptions;
using TestFramework.Logging;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace TestFramework.Core
{

    /// <summary>
    /// Компонент: позволяет выполнять действия в отложенном режиме, поддерживает ицициализацию в декларативном стиле 
    /// </summary>
    //[Injectable]
    public interface IComponent
    {
        /// <summary>
        /// Мета-информация о компоненте - родительский компонет, атрибуты
        /// </summary>
        IMetaInfo MetaInfo { get; }

        /// <summary>
        /// Инициализирует компонент мета-информацией о нем, такой как название поля, атрибуты, родительский компонент и т.д
        /// </summary>
        /// <param name="info">Мета-информация о компоненте</param>
        void Initialize();

        T Do<T>(Func<T> func, [CallerMemberName] string caller = "");
        void Do(Action action, [CallerMemberName] string caller = "");
        IDisposable On(string actionName, Action action);

        bool Skip { get; set; }
        object Default { get; set; }
    }

    [Importable]
    public class Component : IComponent
    {
        private IMetaInfo metaInfo = new MetaInfo();
        private ConcurrentDictionary<string, Action> on = new ConcurrentDictionary<string, Action>();

        public IMetaInfo MetaInfo => metaInfo ?? throw new ComponentNotInitializedException($"Компонент \"{GetType().Name}\" не проинициализирован");


        [Import] protected ILogService Log;

        void IComponent.Initialize()
        {
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public bool Skip { get; set; }
        public object Default { get; set; }

        public IDisposable On(string actionName, Action action)
        {
            on.AddOrUpdate(actionName, action, (k, v) => v += action);
            return new Unsubscriber(() => on.TryRemove(actionName, out var value));
        }

        public void Do(Action action, [CallerMemberName] string caller = "")
        {
            action();
            if (on.TryGetValue(caller, out var afterAction))
                afterAction?.Invoke();
            
        }
        public T Do<T>(Func<T> func, [CallerMemberName] string caller = "")
        {
            var result = func();
            if (on.TryGetValue(caller, out var afterAction))
                afterAction?.Invoke();
            return result;            
        }

        protected void Do(string description, Action action) => Do(() =>
        {
            action();
            Log.Info(description);
        });

        protected T Do<T>(string description, Func<T> func) => Do(() =>
        {
            var result = func();
            Log.Info(description);
            return result;
        });

        protected void Step(string description, Action action, [CallerMemberName] string caller = "") => Do(() =>
        {
            using (Log.NewSession(description, LogLevel.Info))
            {
                action?.Invoke();
            }
        });

        protected T Step<T>(string description, Func<T> func, [CallerMemberName] string caller = "") => Do(() =>
        {
            using (Log.NewSession(description, LogLevel.Info))
            {
                return func();
            }
        });

        public override string ToString()
        {
            return metaInfo.GetAttribute<NameAttribute>()?.Name ?? metaInfo.Name ?? GetType().Name;
        }
    }

    public class Unsubscriber : IDisposable
    {
        private readonly Action unsubscribe;

        public Unsubscriber(Action unsubscribe)
        {
            this.unsubscribe = unsubscribe;
        }

        public void Dispose()
        {
            unsubscribe?.Invoke();
        }
    }

}

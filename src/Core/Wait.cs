using TestFramework.Core.Exceptions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestFramework.Core
{
    public interface IWait<T>
    {
        IWait<T> OnFail(Action<Exception> action);
        T Until(Expression<Func<T, bool>> predicate);
        IWait<T> OnSuccess(Action<T> action);
        IWait<T> Timeout(TimeSpan timeout);
        IWait<T> Interval(TimeSpan interval);
    }

    public class Wait<T> : IWait<T>
    {
        public Wait(string desctiption, Func<T> getValue)
        {
            this.desctiption = desctiption;
            this.getValue = getValue;
        }
        
        private readonly string desctiption;
        private readonly Func<T> getValue;
        private TimeSpan timeout = TimeSpan.FromSeconds(10);
        private TimeSpan interval = TimeSpan.FromMilliseconds(100);
        private Action<T> success = delegate { };
        private Action<Exception> fail = delegate { };

        public IWait<T> OnFail(Action<Exception> action)
        {
            fail += action;
            return this;
        }

        public T Until() => Until(x => true);

        public T Until(Expression<Func<T, bool>> predicate)
        {
            var predicateDescription = new ExpressionDescriptionGeneratorHelper().GetDescription(predicate);
            var end = DateTime.Now + timeout;
            Exception exception = null;
            while (DateTime.Now < end)
            {
                var predicateFunc = predicate.Compile();
                try
                {
                    var result = getValue();
                    if (predicateFunc(result))
                    {
                        success(result);
                        return result;
                    }
                    else
                        throw new TestFrameworkException($"Не выполнено условие \"{predicateDescription}\"");
                }
                catch (Exception ex)
                {
                    exception = ex;
                    fail(ex);
                }
                Task.Delay(interval).Wait();
            }

            throw new TestFrameworkException($"Не удалось выполнить {desctiption} за {timeout}", exception);
        }

        public IWait<T> OnSuccess(Action<T> action)
        {
            success += action;
            return this;
        }

        public IWait<T> Timeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public IWait<T> Interval(TimeSpan interval)
        {
            this.interval = interval;
            return this;
        }
    }

    public static class WaitExtensions
    {
        public static IWait<TComponent> Wait<TComponent>(this TComponent component) where TComponent : IComponent
        {
            return new Wait<TComponent>($"Ожидание {component}", () => component);
        }

        public static T Until<T>(this IWait<T> wait)
        {
            return wait.Until(x => true);
        }
    }
}

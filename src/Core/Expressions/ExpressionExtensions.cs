using System;
using System.Linq.Expressions;

namespace TestFramework.Core
{
    public static class ExpressionExtensions
    {
        public static object GetValue(this Expression expression)
        {
            return Expression.Lambda<Func<object>>(Expression.Convert(expression, typeof(object))).Compile().Invoke();
        }

        public static object GetValue<T>(this Expression expression, ParameterExpression parameter, T obj)
        {
            return Expression.Lambda<Func<T, object>>(expression, parameter).Compile().Invoke(obj);
        }
    }
}

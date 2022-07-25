
using TestFramework.Logging;
using System;
using System.Linq.Expressions;

namespace TestFramework.Core
{
    public interface IAssert
    {
        ILogService Log { get; }
    }

    public class AssertService : IAssert
    {
        public AssertService(ILogService log)
        {
            Log = log;
        }

        public ILogService Log { get; }
    }

    public static class AssertExtensions
    {
        public static string GetString(this Expression expression)
        {
            var expressionHelper = new ExpressionDescriptionGeneratorHelper();
            return expressionHelper.GetDescription(expression);
        }

        public static string GetString<T>(this Expression expression, T value)
        {
            var expressionHelper = new ExpressionDescriptionGeneratorHelper();
            return expressionHelper.GetDescription(expression, value);
        }

        public static void AreEquals(this IAssert assert, object actual, object expected)
            => assert.IsTrue(() => actual?.Equals(expected) ?? false,
                successText: $"\"{actual}\" == \"{expected}\"",
                failText: $"\"{actual}\" != \"{expected}\"");

        public static void AreEquals(this IAssert assert, Expression<Func<object>> actual, object expected)
            => assert.IsTrue(() => actual?.Compile()?.Invoke()?.Equals(expected) ?? false,
                successText: $"\"{actual.GetString()}\" == \"{expected}\"",
                failText: $"\"{actual.GetString()}\" != \"{expected}\"");

        public static void AreEquals(this IAssert assert, Expression<Func<object>> actual, Expression<Func<object>> expected)
            => assert.IsTrue(() => actual?.Compile()?.Invoke().Equals(expected?.Compile()?.Invoke()) ?? false,
                successText: $"\"{actual.GetString()}\" == \"{expected.GetString()}\"",
                failText: $"\"{actual.GetString()}\" != \"{expected.GetString()}\"");

        public static void IsTrue(this IAssert assert, Expression<Func<bool>> expression)
            => assert.IsTrue(expression.Compile(),
               successText: expression.GetString(),
               failText: $"Условие не выполнено: {expression}");

        public static void IsTrue(this IAssert assert, Func<bool> expression, string successText, string failText)
        {
            if (!expression())
                throw new Exception(failText);
            assert.Log.Info($"Проверка: {successText}");
        }
    }
}

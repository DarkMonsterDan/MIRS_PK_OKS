using System;
using System.Linq;
using System.Linq.Expressions;

namespace TestFramework.Core
{
    public interface IExpressionDescriptionGeneratorHelper
    {
        string GetDescription<T>(Expression expression, T value);
        string GetDescription(Expression expression);
    }

    public class ExpressionDescriptionGeneratorHelper : IExpressionDescriptionGeneratorHelper
    {
        public ExpressionSettings Settings { get; set; } = new ExpressionSettings();

        public string GetDescription<T>(Expression expression, T value) 
            => GetDescription(new DescriptionGeneratorExpressionVisitor<T>(value, Settings), expression);

        public string GetDescription(Expression expression) 
            => GetDescription(new DescriptionGeneratorExpressionVisitor(Settings), expression);

        string GetDescription(ExpressionVisitor visitor, Expression expression)
        {
            var resultExpression = visitor.Visit(expression);
            if (resultExpression is LambdaExpression lambdaExpression)
                return GetString(lambdaExpression.Body);
            return GetString(resultExpression);
        }

        string GetString(Expression expression)
        {
            var result = expression.ToString();
            foreach(var replace in Settings.Replaces)
            {
                result = result.Replace(replace.Key, replace.Value);
            }

            return result;
        }
    }

}

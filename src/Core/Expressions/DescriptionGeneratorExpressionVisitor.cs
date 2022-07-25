using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestFramework.Core
{
    public class DescriptionGeneratorExpressionVisitor : ExpressionVisitor
    {
        private readonly ExpressionSettings settings;

        public DescriptionGeneratorExpressionVisitor(ExpressionSettings settings)
        {
            this.settings = settings;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var members = GetMembers(node);
            var name = GetFullName(node);
            if (members.Last().Expression is ParameterExpression parameter)
            {
                return Expression.Parameter(node.Type, name);
            }

            var value = node.GetValue();

            return Expression.Parameter(node.Type, $"{name}: {value}");
        }

        protected virtual string GetParameterName(ParameterExpression parameter)
        {
            return parameter.Type.GetCustomAttributes(true).OfType<NameAttribute>().FirstOrDefault()?.Name ?? parameter.Name;
        }

        public string GetFullName(MemberExpression expression)
        {
            var members = GetMembers(expression);
            var nameBuilder = new StringBuilder();

            if (members.Last().Expression is ParameterExpression parameter)
            {
                var parameterName = GetParameterName(parameter);
                nameBuilder.Append(parameterName);
            }

            foreach (var member in members.Reverse())
            {
                if (member.Member.GetCustomAttributes(true).OfType<ParentNameAttribute>().FirstOrDefault() != null)
                    continue;
                var memberName = member.Member.GetCustomAttributes(true).OfType<NameAttribute>().FirstOrDefault()?.Name
                    ?? member.Type.GetCustomAttributes(true).OfType<NameAttribute>().FirstOrDefault()?.Name
                    ?? member.Member.Name;
                if (nameBuilder.Length > 0)
                    nameBuilder.Append(settings.MemberSeparator);
                nameBuilder.Append(memberName);
            };

            var name = nameBuilder.ToString();

            return name;
        }

        public IEnumerable<MemberExpression> GetMembers(MemberExpression expression)
        {
            yield return expression;
            while (expression.Expression is MemberExpression parentExpression)
            {
                expression = parentExpression;
                yield return expression;
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var name = node.Type.GetCustomAttributes(true).OfType<NameAttribute>().FirstOrDefault()?.Name;
            if (name != null)
                return Expression.Parameter(node.Type, name);
            return base.VisitParameter(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var expression = base.VisitBinary(node);
            return expression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var name = node.Method.GetCustomAttributes(true).OfType<NameAttribute>().FirstOrDefault()?.Name;
            
            if (name == null)
                return base.VisitMethodCall(node);

            var arguments = node.Arguments;
            var parameters = node.Method.GetParameters();

            for (var i = 0; i < arguments.Count; i++)
            {
                var argument = arguments[i];
                string argumentText = null;
                if (argument is MemberExpression member)
                {
                    var members = GetMembers(member).ToList();
                    if (members.Last().Expression is ParameterExpression)
                    {
                        argumentText = GetFullName(member);
                    }
                }

                if (argumentText == null)
                {
                    argumentText = argument.GetValue().ToString();
                }
                name = name.Replace($"{{{parameters[i].Name}}}", argumentText);
            }

            if (node.Object is MemberExpression methodMembers)
            {
                var members = GetMembers(methodMembers);
                var memberName = GetFullName(methodMembers);
                name = $"{memberName}{settings.MemberSeparator}{name}";
            }

            return Expression.Parameter(node.Type, name);
        }
    }

    public class DescriptionGeneratorExpressionVisitor<T> : DescriptionGeneratorExpressionVisitor
    {
        private readonly T value;
        private readonly ExpressionSettings settings;

        public DescriptionGeneratorExpressionVisitor(T value, ExpressionSettings settings) : base(settings)
        {
            this.value = value;
            this.settings = settings;
        }

        protected override string GetParameterName(ParameterExpression parameter)
        {
            return parameter.GetValue(parameter, value).ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var members = GetMembers(node).ToList();
            var name = GetFullName(node);

            if (members.Last().Expression is ParameterExpression parameter)
            {
                var parameterValue = node.GetValue(parameter, value);
                if (name != null)
                    return Expression.Parameter(node.Type, $"{name}: {parameterValue}");
            }

            var v = node.GetValue();
            return Expression.Parameter(node.Type, $"{name}: {v}");
        }
    }
}

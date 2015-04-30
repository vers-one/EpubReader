using System;
using System.Linq.Expressions;

namespace EpubReaderDemo.Utils
{
    public static class ExpressionUtils
    {
        public static string GetPropertyName(LambdaExpression expression)
        {
            return expression.Body is MemberExpression
                ? (expression.Body as MemberExpression).Member.Name
                : (((UnaryExpression)expression.Body).Operand as MemberExpression).Member.Name;
        }

        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyName(expression as LambdaExpression);
        }
    }
}

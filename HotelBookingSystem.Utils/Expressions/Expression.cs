using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Utils.Expressions
{
    public static class ExpressionNew
    {
        public static Expression<Func<T, bool>> AndExpression<T>(this
        Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var visitor = new ParameterReplaceVisitor()
            {
                Target = right.Parameters[0],
                Replacement = left.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(right.Body);
            var andExpression = Expression.AndAlso(left.Body, rewrittenRight);
            return Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters);
        }
        public static Expression<Func<T, bool>> OrExpression<T>(this
        Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var visitor = new ParameterReplaceVisitor()
            {
                Target = right.Parameters[0],
                Replacement = left.Parameters[0],
            };

            var rewrittenRight = visitor.Visit(right.Body);
            var orExpression = Expression.OrElse(left.Body, rewrittenRight);
            return Expression.Lambda<Func<T, bool>>(orExpression, left.Parameters);
        }
    }
}

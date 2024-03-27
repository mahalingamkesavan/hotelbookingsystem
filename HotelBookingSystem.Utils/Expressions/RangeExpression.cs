using System.Linq.Expressions;

namespace HotelBookingSystem.Utils.Expressions
{
    public partial class ExpressionBuilder
    {
        public BinaryExpression RangePredicate<T>(string columnName, int min = 0, int max = int.MaxValue)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var minConstant = Expression.Constant(min, typeof(int));
            var maxConstant = Expression.Constant(max, typeof(int));
            var property = Expression.Property(parameterExpression, columnName);

            //if (max == int.MaxValue && min == 0)
            //    return null;
            //else if (max == int.MaxValue)
            //    return Expression.GreaterThanOrEqual(property, minConstant);
            //else if (min == 0)
            //    return Expression.LessThanOrEqual(property, maxConstant);
            //else
            return Expression.AndAlso
                (
                    Expression.GreaterThanOrEqual(property, minConstant),
                    Expression.LessThanOrEqual(property, maxConstant)
                );
        }
    }
}

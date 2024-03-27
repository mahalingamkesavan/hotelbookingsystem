using System.Linq.Expressions;

namespace HotelBookingSystem.Utils.Expressions
{
    public partial class ExpressionBuilder
    {
        public BinaryExpression EqualPredicate<T>(string columnName, object searchValue)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var constant = Expression.Constant(searchValue);
            var property = Expression.Property(parameterExpression, columnName);
            return Expression.Equal(property, constant);
        }
    }
}

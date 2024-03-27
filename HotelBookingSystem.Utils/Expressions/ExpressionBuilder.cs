using System.Linq.Expressions;
using System.Reflection;

namespace HotelBookingSystem.Utils.Expressions
{
    public partial class ExpressionBuilder
    {
        public Expression<Func<T, bool>> MethodPredicate<T>(string MethodName, string columnName, object searchValue)
        {
            var type = typeof(T);
            var x = Expression.Parameter(type, "x");
            var member = Expression.Property(x, columnName);
            ConstantExpression constant;

            if (searchValue is string)
            {
                MethodInfo? method = typeof(string).GetMethod(MethodName, new[] { typeof(string) });
                constant = Expression.Constant(searchValue, typeof(string));
                var call = Expression.Call(member, method, constant);
                return Expression.Lambda<Func<T, bool>>(call, x);
            }
            else
            {
                MethodInfo? method = typeof(string).GetMethod(MethodName, new[] { typeof(int) });
                constant = Expression.Constant(searchValue, typeof(int));
                var call = Expression.Call(member, method, constant);
                return Expression.Lambda<Func<T, bool>>(call, x);
            }

        }
    }
}

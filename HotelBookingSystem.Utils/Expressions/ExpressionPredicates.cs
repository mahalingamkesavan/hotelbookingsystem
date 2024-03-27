using System.Linq.Expressions;
using System.Reflection;

namespace HotelBookingSystem.Utils.Expressions
{
    public class ExpressionPredicates<T>
    {
        class ExpressionCriterion
        {
            public ExpressionCriterion
            (
                string propertyName,
                object value,
                ExpressionType operatorTYpe,
                string andOr = "And"
            )
            {
                AndOr = andOr;
                PropertyName = propertyName;
                Value = value;
                Operator = operatorTYpe;
                validateProperty(typeof(T), propertyName);
            }
            public ExpressionCriterion
            (
                string propertyName,
                object value,
                string methodName,
                string andOr = "And"
            )
            {
                AndOr = andOr;
                PropertyName = propertyName;
                Value = value;
                MethodName = typeof(string).GetMethod(methodName, new[] { typeof(string) });
                Operator = ExpressionType.Call;
                validateProperty(typeof(T), propertyName);
            }
            PropertyInfo validateProperty(Type type, string propertyName)
            {
                string[] parts = propertyName.Split('.');

                var info = (parts.Length > 1)
                ? validateProperty(
                    type.GetProperty(
                        parts[0]).PropertyType,
                        parts.Skip(1).Aggregate((a, i) =>
                            a + "." + i)) : type.GetProperty(propertyName);
                if (info == null)
                    throw new ArgumentException(propertyName,
                        $"Property {propertyName} is not a member of {type.Name}");
                return info;
            }
            public string PropertyName { get; }
            public object Value { get; }
            public ExpressionType Operator { get; }
            public string AndOr { get; }
            public MethodInfo MethodName { get; } = null;
        }
        List<ExpressionCriterion> _expressionCriterion = new
        List<ExpressionCriterion>();

        private string _andOr = "And";
        public ExpressionPredicates<T> And()
        {
            _andOr = "And";
            return this;
        }
        public ExpressionPredicates<T> Or()
        {
            _andOr = "Or";
            return this;
        }
        public ExpressionPredicates<T> Add
            (
            string propertyName,
            object value, ExpressionType operatorTYpe
            )
        {
            var newCriterion = new ExpressionCriterion
                (
                propertyName, value,
                operatorTYpe, _andOr
                );
            _expressionCriterion.Add(newCriterion);
            return this;
        }
        public ExpressionPredicates<T> Add
            (
            string propertyName,
            object value, string methodName
            )
        {
            var newCriterion = new ExpressionCriterion
                (
                propertyName, value,
                methodName, _andOr
                );
            _expressionCriterion.Add(newCriterion);
            return this;
        }
        Expression GetExpression
            (
            ParameterExpression parameter,
            ExpressionCriterion ExpressionCriteria
            )
        {
            Expression expression = parameter;
            foreach (var member in ExpressionCriteria.PropertyName.Split('.'))
            {
                expression = Expression.PropertyOrField(expression, member);
            }
            if (ExpressionCriteria.MethodName == null)
                return Expression.MakeBinary(ExpressionCriteria.Operator,
                    expression, Expression.Constant(ExpressionCriteria.Value));
            else
            {
                ConstantExpression constant = Expression.Constant(ExpressionCriteria.Value, typeof(string));
                return Expression.Call(expression, ExpressionCriteria.MethodName, constant);
            }
        }
        public Expression<Func<T, bool>> GetLambda()
        {
            Expression expression = null;
            var parameterExpression = Expression.Parameter(typeof(T),
                    typeof(T).Name.ToLower());
            foreach (var item in _expressionCriterion)
            {
                if (expression == null)
                {
                    expression = GetExpression(parameterExpression, item);
                }
                else
                {
                    expression = item.AndOr == "And" ?
                        Expression.And(expression,
                            GetExpression(parameterExpression, item)) :
                        Expression.Or(expression,
                            GetExpression(parameterExpression, item));
                }
            }
            return expression != null ?
                Expression.Lambda<Func<T,
                    bool>>(expression, parameterExpression) : null;
        }
    }

}

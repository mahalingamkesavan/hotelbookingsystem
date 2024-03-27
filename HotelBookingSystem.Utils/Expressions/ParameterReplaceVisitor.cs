using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Utils.Expressions
{
    public class ParameterReplaceVisitor : ExpressionVisitor
    {
        public ParameterExpression Target { get; set; } = null!;
        public ParameterExpression Replacement { get; set; } = null!;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == Target ? Replacement : base.VisitParameter(node);
        }
    }
}

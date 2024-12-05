using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;

namespace Raika.Common.SharedApplicationServices.Common
{    
    public class PredicateExpressionVisitor<T>
    {
        public System.Text.StringBuilder Condition { get; } = new System.Text.StringBuilder();

        public string ConvertToSql(Expression<Func<T, bool>> expression) 
        {
            var body = expression.Body as BinaryExpression;
            if (body == null)
                throw new InvalidOperationException("Expression must be a binary expression.");

            if (
                body.NodeType == ExpressionType.Equal ||
                body.NodeType == ExpressionType.GreaterThan ||
                body.NodeType == ExpressionType.GreaterThanOrEqual ||
                body.NodeType == ExpressionType.LessThan ||
                body.NodeType == ExpressionType.LessThanOrEqual ||
                body.NodeType == ExpressionType.NotEqual
                )
            {
                var value = Expression.Lambda(body.Right).Compile().DynamicInvoke();

                if (body.Left is MemberExpression)
                {
                    var leftMember = body.Left as MemberExpression;
                    var leftName = leftMember!.Member.Name;
                    var condition = $"[{leftName}] {GetSqlOperator(body.NodeType)} N'{value}'";
                    Condition.Append(condition);
                }
                else
                {
                    var op = ((UnaryExpression)body.Left).Operand;
                    var leftName = ((MemberExpression)op).Member.Name;
                    var condition = $"[{leftName}] {GetSqlOperator(body.NodeType)} N'{value}'";
                    Condition.Append(condition);
                }



            }
            else if (body.NodeType == ExpressionType.AndAlso || body.NodeType == ExpressionType.OrElse)
            {
                var left = body.Left as BinaryExpression;
                var right = body.Right as BinaryExpression;

                if (left == null || right == null)
                    throw new InvalidOperationException("Expression not supported.");

                var leftMember = left.Left as MemberExpression;
                var leftName = leftMember!.Member.Name;
                var leftValue = Expression.Lambda(left.Right).Compile().DynamicInvoke();
                if(leftValue == null)
                    Condition.Append($"[{leftName}] IS NULL");
                else
                {
                    var conditionLeftValue = GetConditionValueIsString(left.Right.Type.Name) ? $"N'{leftValue}'" : $"{leftValue}";
                    Condition.Append($"[{leftName}] {GetSqlOperator(left.NodeType)} {conditionLeftValue}");
                }               

                Condition.Append(GetSqlOperator(body.NodeType));

                var rightMember = right.Left as MemberExpression;
                var rightName = rightMember!.Member.Name;
                var rightValue = Expression.Lambda(right.Right).Compile().DynamicInvoke();
                if (rightValue == null)
                    Condition.Append($"[{rightName}] IS NULL");
                else
                {
                    var conditionRightValue = GetConditionValueIsString(right.Right.Type.Name) ? $"N'{rightValue}'" : $"{rightValue}";
                    Condition.Append($"[{rightName}] {GetSqlOperator(right.NodeType)} {conditionRightValue}");
                }
            }

            var result = Condition.ToString();
            result = result.Replace("True", "1");
            result = result.Replace("true", "1");
            result = result.Replace("False", "0");
            result = result.Replace("false", "0");

            return result;
        }
        
        private static bool GetConditionValueIsString(string name) 
        {
            switch (name)
            {
                case "String":
                case "Guid":
                case "DateTime":
                    return true;
                case "Int16":
                case "Int32":
                case "Int64":
                case "Boolean":
                    return false;
                default:
                    return false;
            }
        }
        private static string GetSqlOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return " = ";
                case ExpressionType.NotEqual:
                    return " <> ";
                case ExpressionType.GreaterThan:
                    return " > ";
                case ExpressionType.GreaterThanOrEqual:
                    return " >= ";
                case ExpressionType.LessThan:
                    return " < ";
                case ExpressionType.LessThanOrEqual:
                    return " <= ";
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return " AND ";
                case ExpressionType.OrElse:
                case ExpressionType.Or:
                    return " OR ";
                default:
                    throw new NotSupportedException($"Unsupported expression type: {expressionType}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
	public class ExpressionToFTSRequestTranslator : ExpressionVisitor
	{
		IList<StringBuilder> resultStrings;

		public IEnumerable<string> Translate(Expression exp)
		{
			resultStrings = new List<StringBuilder> { new StringBuilder() };
			Visit(exp);

			return resultStrings.Select(sb => sb.ToString());
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.DeclaringType == typeof(Queryable)
				&& node.Method.Name == "Where")
			{
				var predicate = node.Arguments[1];
				Visit(predicate);

				return node;
			}

    if (node.Method.DeclaringType == typeof(string) &&
        node.Method.Name == "StartsWith" || node.Method.Name == "EndsWith" || node.Method.Name == "Contains")
    {
      var constantExpression = (ConstantExpression) node.Arguments[0];
      var modifiedExpression = GetExpressionForStringMethod(constantExpression, node.Method.Name);
      var binaryExpression = Expression.Equal(node.Object, modifiedExpression);

      Visit(binaryExpression);

      return node;
    }

			return base.VisitMethodCall(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Equal:
          MemberExpression memberExpression = null;
          ConstantExpression constantExpression = null;

          if (node.Left.NodeType == ExpressionType.MemberAccess)
          {
            memberExpression = (MemberExpression) node.Left;
          }
          else if (node.Left.NodeType == ExpressionType.Constant)
          {
            constantExpression = (ConstantExpression) node.Left;
          }

          if (node.Right.NodeType == ExpressionType.MemberAccess)
          {
            memberExpression = (MemberExpression) node.Right;
          }
          else if (node.Right.NodeType == ExpressionType.Constant)
          {
            constantExpression = (ConstantExpression) node.Right;
          }

          if (memberExpression == null || constantExpression == null)
          {
            throw new NotSupportedException("One of the operands should be property or field, another should be constant");
          }

					Visit(memberExpression);
					resultStrings.Last().Append("(");
					Visit(constantExpression);
					resultStrings.Last().Append(")");
					break;

        case ExpressionType.AndAlso:
          Visit(node.Left);
          resultStrings.Add(new StringBuilder());
          Visit(node.Right);
          break;

				default:
					throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
			};

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			resultStrings.Last().Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			resultStrings.Last().Append(node.Value);

			return node;
		}

    private ConstantExpression GetExpressionForStringMethod(ConstantExpression expression, string methodName)
    {
      switch (methodName)
      {
        case "StartsWith":
          return Expression.Constant(expression.Value + "*");
        case "EndsWith":
          return Expression.Constant("*" + expression.Value);
        case "Contains":
          return Expression.Constant("*" + expression.Value + "*");
        default:
          throw new NotSupportedException(string.Format("String method {0} is not supported", methodName));
      }
    }
	}
}

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
		StringBuilder resultString;

		public string Translate(Expression exp)
		{
			resultString = new StringBuilder();
			Visit(exp);

			return resultString.ToString();
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
					resultString.Append("(");
					Visit(constantExpression);
					resultString.Append(")");
					break;

				default:
					throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
			};

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			resultString.Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			resultString.Append(node.Value);

			return node;
		}
	}
}

using System;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace whoop
{
    class MyTranslator : ExpressionVisitor
    {
        private StringBuilder Output { get; } = new StringBuilder();

        public string Query
        {
            get
            {
                RegexOptions options = RegexOptions.None;
                var regex = new Regex("[ ]{2,}", options);    

                return regex.Replace(Output.ToString(), " ");
            }
        }
        
        protected override Expression VisitMember(MemberExpression node)
        {
            var atts = node.Member.GetCustomAttributes(typeof(PropertyTypeAttribute), true);
            if (atts.Length != 0 && atts[0] is PropertyTypeAttribute pta)
            {
                Output.Append(pta.PropertyType);
                return node;
            }
            
            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Output.Append(node.Value);
            return base.VisitConstant(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Equal)
            {
                Output.Append("(eq ");
            }
            else if (node.NodeType == ExpressionType.AndAlso)
            {
                Output.Append("(and ");
            }
            else if (node.NodeType == ExpressionType.OrElse)
            {
                Output.Append("(or ");
            }
            
            Visit(node.Left);
            Output.Append(" ");
            Visit(node.Right);
            Output.Append(") ");

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (!node.Method.Name.Equals("Contains", StringComparison.InvariantCultureIgnoreCase))
                return base.VisitMethodCall(node);

            if (node.Object.NodeType != ExpressionType.MemberAccess)
            {
                return base.VisitMethodCall(node);
            }

            var source = node.Object as MemberExpression;
            
            Output.Append("(contains ");

            var atts = source.Member.GetCustomAttributes(typeof(PropertyTypeAttribute), true);
            if (atts.Length != 0 && atts[0] is PropertyTypeAttribute pta)
            {
                Output.Append(pta.PropertyType);
            }
            
            Output.Append(" ");
            Output.Append(node.Arguments[0]);
            Output.Append(") ");
            
            return node;
        }
    }
}
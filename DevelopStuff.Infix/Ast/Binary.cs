using System;
using System.Collections.Generic;
using System.Text;
using Tree = Microsoft.Scripting.Ast;
using Microsoft.Scripting;

namespace DevelopStuff.Infix.Ast
{
    public class Binary : Expression
    {
        Operator _op;
        Expression _left;
        Expression _right;

        public Binary(SourceSpan span, Operator op, Expression left, Expression right)
            : base(span)       
        {
            _op = op;
            _left = left;
            _right = right;
        }

        internal override Tree.Expression Gen(InfixGenerator lg)
        {
            Operators op = DlrOperator(_op);

            return Tree.Ast.Action.Operator(
                op,
                typeof(object),
                _left.Gen(lg),
                _right.Gen(lg)
            );
        }

        internal static Tree.AstNodeType GetAstNodeType(Operator op)
        {
            switch (op)
            {
                case Operator.Add: return Tree.AstNodeType.Add;
                case Operator.Subtract: return Tree.AstNodeType.Subtract;
                case Operator.Divide: return Tree.AstNodeType.Divide;
                case Operator.Multiply: return Tree.AstNodeType.Multiply;
                default:
                    throw new InvalidOperationException();
            }
        }

        internal static Operators DlrOperator(Operator op)
        {
            switch (op)
            {
                case Operator.Add: return Operators.Add;
                case Operator.Subtract: return Operators.Subtract;
                case Operator.Divide: return Operators.Divide;
                case Operator.Multiply: return Operators.Multiply;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static Binary Maek(Operator op, Expression left, Expression right)
        {
            return new Binary(
                new SourceSpan(left.Span.Start, right.Span.End),
                op,
                left,
                right
            );
        }
    }
}

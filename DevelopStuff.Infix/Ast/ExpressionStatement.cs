using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;

using Tree = Microsoft.Scripting.Ast;

namespace DevelopStuff.Infix.Ast
{
    class ExpressionStatement : Statement
    {
        private readonly Expression _expression;

        public ExpressionStatement(SourceSpan span, Expression expression)
            : base(span)
        {
            _expression = expression;
        }

        internal override Tree.Statement Gen(InfixGenerator lg)
        {
            return Tree.Ast.Statement(
                Span,
                Tree.Ast.Call(
                    typeof(InfixOps).GetMethod("Visible"),
                    _expression.Gen(lg)
                )
            );
        }

        internal static ExpressionStatement Maek(Expression expression)
        {
            return new ExpressionStatement(
                expression.Span,
                expression
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Tree = Microsoft.Scripting.Ast;
using Microsoft.Scripting;

namespace DevelopStuff.Infix.Ast
{
    public class Constant : Expression
    {
        object _value;

        public Constant(SourceSpan span, object value)
            : base(span)
        {
            _value = value;
        }

        internal override Tree.Expression Gen(InfixGenerator lg)
        {
            return Tree.Ast.Convert(
                Tree.Ast.Constant(
                    _value
                ),
                typeof(object)
            );
        }

        internal static Constant MaekNumbr(Token value)
        {
            return new Constant(value.Span, Int32.Parse(value.String));
        }

    }
}

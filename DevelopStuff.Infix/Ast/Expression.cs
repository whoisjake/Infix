using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;

using Tree = Microsoft.Scripting.Ast;

namespace DevelopStuff.Infix.Ast
{
    public abstract class Expression : Node
    {
        internal protected Expression(SourceSpan span)
            : base(span)
        {
        }

        internal abstract Tree.Expression Gen(InfixGenerator lg);

        internal virtual Tree.Statement GenSet(SourceSpan span, InfixGenerator lg, Operators op, Tree.Expression right)
        {
            throw new InvalidOperationException("Cannot assign to " + this.GetType().Name);
        }
    }
}

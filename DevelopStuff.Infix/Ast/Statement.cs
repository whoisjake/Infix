using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;

using Tree = Microsoft.Scripting.Ast;

namespace DevelopStuff.Infix.Ast
{
    public abstract class Statement : Node
    {
        internal protected Statement(SourceSpan span)
            : base(span)
        {
        }

        internal abstract Tree.Statement Gen(InfixGenerator gen);
    }
}

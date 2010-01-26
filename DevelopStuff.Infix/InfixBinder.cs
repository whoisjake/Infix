using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting;

using Tree = Microsoft.Scripting.Ast;

namespace DevelopStuff.Infix
{
    public class InfixBinder : ActionBinder
    {
        public InfixBinder(CodeContext context)
            : base(context)
        {
        }

        public override bool CanConvertFrom(Type fromType, Type toType, Microsoft.Scripting.NarrowingLevel level)
        {
            return toType.IsAssignableFrom(fromType);
        }

        public override Microsoft.Scripting.Ast.Expression CheckExpression(Microsoft.Scripting.Ast.Expression expr, Type toType)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public override Microsoft.Scripting.Ast.Expression ConvertExpression(Microsoft.Scripting.Ast.Expression expr, Type toType)
        {
            return Tree.Ast.ConvertHelper(expr, toType);
        }

        public override bool PreferConvert(Type t1, Type t2)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }
    }
}

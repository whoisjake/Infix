using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Ast;
using Microsoft.Scripting;

using Tree = Microsoft.Scripting.Ast;

namespace DevelopStuff.Infix.Ast
{
    public class Block : Statement
    {
        private readonly Statement[] _statements;

        public Block(SourceSpan span, Statement[] statements)
            : base(span)
        {
            _statements = statements;
        }

        internal override Tree.Statement Gen(InfixGenerator lg)
        {
            if (_statements != null)
            {
                Tree.Statement[] statements = new Tree.Statement[_statements.Length];
                for (int i = 0; i < statements.Length; i++)
                {
                    statements[i] = _statements[i].Gen(lg);
                }
                return Tree.Ast.Block(
                    statements
                );
            }
            else
            {
                return Tree.Ast.Empty();
            }
        }

        public static Block Maek(List<Statement> statements)
        {
            if (statements != null && statements.Count > 0)
            {
                return new Block(
                    new SourceSpan(
                        statements[0].Span.Start,
                        statements[statements.Count - 1].Span.End),
                    statements.ToArray()
                );
            }
            return null;
        }
    }
}

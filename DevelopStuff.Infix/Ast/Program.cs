using System;
using System.Collections.Generic;
using System.Text;
using Tree = Microsoft.Scripting.Ast;
using Microsoft.Scripting;

namespace DevelopStuff.Infix.Ast
{
    public class Program : Statement
    {
        private SourceSpan _start;
        private SourceSpan _end;
        private Statement _body;

        public Program(SourceSpan span, SourceSpan start, SourceSpan end, Statement body)
            : base(span)
        {
            _start = start;
            _end = end;
            _body = body;
        }

        internal override Tree.Statement Gen(InfixGenerator lg)
        {
            return Tree.Ast.Block(
                Tree.Ast.Empty(_start),
                _body != null ? _body.Gen(lg) : Tree.Ast.Empty(),
                Tree.Ast.Empty(_end)
            );
        }

        internal static Program Maek(Statement statement)
        {
            List<Statement> body = new List<Statement>();
            body.Add(statement);
            Block block = Block.Maek(body);

            if(block != null)
            {
                return new Program(block.Span, block.Span, block.Span, block);
            }

            return new Program(SourceSpan.None,SourceSpan.None,SourceSpan.None,block);
        }
    }
}

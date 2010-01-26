using System;
using System.Collections.Generic;
using System.Text;

using Tree = Microsoft.Scripting.Ast;
using DevelopStuff.Infix.Ast;
using Microsoft.Scripting;

namespace DevelopStuff.Infix
{
 
    public class InfixGenerator
    {
        private Tree.CodeBlock _block;
        private Dictionary<string, Tree.Variable> _locals = new Dictionary<string, Tree.Variable>();

        public InfixGenerator(Tree.CodeBlock block)
        {
            _block = block;
        }

        internal static Tree.CodeBlock Generate(Statement statement, string name)
        {
            Tree.CodeBlock block = Tree.Ast.CodeBlock(name ?? "<interactive>");
            block.Body = statement.Gen(new InfixGenerator(block));
            return block;
        }

        internal Tree.Variable GetLocal(string name)
        {
            Tree.Variable variable;
            if (!_locals.TryGetValue(name, out variable))
            {
                _locals[name] = variable = _block.CreateLocalVariable(new SymbolId(1),
                    typeof(object)
                );
            }
            return variable;
        }
    }
}

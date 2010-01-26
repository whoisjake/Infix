using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Hosting;
using DevelopStuff.Infix.Ast;

namespace DevelopStuff.Infix
{
    public class InfixLanguageContext : LanguageContext
    {
        private InfixEngine _engine;

        public InfixEngine InfixEngine
        {
            get { return this._engine; }
            set { this._engine = value; }
        }

        public override ScriptEngine Engine
        {
            get
            {
                return this._engine;
            }
        }

        public override CodeBlock ParseSourceCode(CompilerContext context)
        {
            InfixParser parser = new InfixParser();

            Program tree = parser.Parse(context);
            if (tree != null)
            {
                return InfixGenerator.Generate(tree, context.SourceUnit.Id);
            }
            else
            {
                return null;
            }
        }
    }
}

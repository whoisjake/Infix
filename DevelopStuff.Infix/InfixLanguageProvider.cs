using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Shell;

namespace DevelopStuff.Infix
{
    public class InfixLanguageProvider : LanguageProvider
    {
        public InfixLanguageProvider(ScriptDomainManager manager)
            : base(manager)
        {
        }

        public override string LanguageDisplayName
        {
            get { return "Infix"; }
        }

        public override ScriptEngine GetEngine(EngineOptions options)
        {
            if (options == null)
            {
                options = new InfixEngineOptions();
            }
            return new InfixEngine(this, options);
        }

        public override OptionsParser GetOptionsParser()
        {
            return new InfixOptionsParser();
        }

        public override CommandLine GetCommandLine()
        {
            return new CommandLine();
        }
    }
}

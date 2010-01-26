using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;

namespace DevelopStuff.Infix
{
    /// <summary>
    /// 
    /// </summary>
    public class InfixEngine : ScriptEngine
    {
        private readonly InfixBinder _binder;

        public InfixEngine(InfixLanguageProvider provider, EngineOptions options)
            : base(provider, options, new InfixLanguageContext())
        {
            ((InfixLanguageContext)LanguageContext).InfixEngine = this;
            this._binder = new InfixBinder(new CodeContext(new Scope(), LanguageContext, new ModuleContext(null)));
        }

        public override ActionBinder DefaultBinder
        {
            get { return this._binder; }
        }

        protected override LanguageContext GetLanguageContext(CompilerOptions compilerOptions)
        {
            return this.LanguageContext;
        }
        protected override LanguageContext GetLanguageContext(ScriptModule module)
        {
            return this.LanguageContext;
        }

        protected override void PrintInteractiveCodeResult(object obj)
        {
            Console.WriteLine("=> {0}",obj);
        }
    }
}

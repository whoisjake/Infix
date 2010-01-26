using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace DevelopStuff.Infix.Console
{
    class Program
    {
        public class Host : ConsoleHost
        {
            protected override void Initialize()
            {
                base.Initialize();

                Options.LanguageProvider =
                    ScriptEnvironment.GetEnvironment().GetLanguageProvider(
                        typeof(InfixLanguageProvider)
                    );
            }

            static void Main(string[] args)
            {
                new Host().Run(args);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Shell;

namespace DevelopStuff.Infix
{
    public class InfixOptionsParser : OptionsParser
    {
        private ConsoleOptions _consoleOptions;
        private EngineOptions _engineOptions;

        public InfixOptionsParser()
        {
            this._consoleOptions = this.GetDefaultConsoleOptions();
            this._engineOptions = this.GetDefaultEngineOptions();
        }

        public override ConsoleOptions ConsoleOptions
        {
            get { return this._consoleOptions; }
            set { this._consoleOptions = value; }
        }
        public override EngineOptions EngineOptions
        {
            get { return this._engineOptions; }
            set { this._engineOptions = value; }
        }

    }
}

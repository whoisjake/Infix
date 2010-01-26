using System;
using Microsoft.Scripting;

namespace DevelopStuff.Infix
{
    class InfixSyntaxErrorException : Exception
    {
        private bool _eof;

        public bool Eof
        {
            get { return _eof; }
        }

        public InfixSyntaxErrorException(string message, bool eof)
            : base(message)
        {
            _eof = eof;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using gppg;

namespace DevelopStuff.Infix
{
    public abstract class ScanBase : IScanner<InfixValue, LexLocation>
    {
        private LexLocation __yylloc;
        protected bool _eof;

        public override LexLocation yylloc
        {
            get { return __yylloc; }
            set { __yylloc = value; }
        }

        public override void yyerror(string format, params object[] args)
        {
            throw new InfixSyntaxErrorException(String.Format(format, args), _eof);
        }
    }
}

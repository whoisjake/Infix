using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;

namespace DevelopStuff.Infix
{
    internal class Token
    {
        Tokens _kind;
        SourceSpan _span;
        string _string;

        public SourceSpan Span
        {
            get { return _span; }
        }

        public String String
        {
            get { return _string; }
        }

        internal Tokens Kind
        {
            get { return _kind; }
        }

        public Token(Tokens kind, SourceSpan span)
        {
            _kind = kind;
            _span = span;
        }

        public Token(Tokens kind, string value, SourceSpan span)
            : this(kind, span)
        {
            _string = value;
        }

        public static Token Maek(Tokens kind, int tokLin, int tokCol, int tokELin, int tokECol)
        {
            return new Token(
                kind,
                new SourceSpan(new SourceLocation(1, tokLin, tokCol + 1), new SourceLocation(1, tokELin, tokECol + 1))
            );
        }

        public static Token Maek(Tokens kind, String value, int tokLin, int tokCol, int tokELin, int tokECol)
        {
            return new Token(
                kind,
                value,
                new SourceSpan(new SourceLocation(1, tokLin, tokCol + 1), new SourceLocation(1, tokELin, tokECol + 1))
            );
        }

        public static Token MaekString(Tokens kind, String value, int tokLin, int tokCol, int tokELin, int tokECol)
        {
            return new Token(
                kind,
                value.Substring(1, value.Length - 2),
                new SourceSpan(new SourceLocation(1, tokLin, tokCol + 1), new SourceLocation(1, tokELin, tokECol + 1))
            );
        }
    }
}

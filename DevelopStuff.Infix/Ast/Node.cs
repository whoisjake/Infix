using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;

namespace DevelopStuff.Infix.Ast
{
    public class Node
    {
        private SourceSpan _span;

        public SourceSpan Span
        {
            get { return _span; }
        }

        internal protected Node(SourceSpan span)
        {
            _span = span;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DevelopStuff.Infix.Ast;

namespace DevelopStuff.Infix
{
    public struct InfixValue
    {
        private object _value;

        public Node Node
        {
            get { return (Node)_value; }
        }

        public Expression Expression
        {
            get { return (Expression)_value; }
            set { _value = value; }
        }

        public Statement Statement
        {
            get { return (Statement)_value; }
            set { _value = value; }
        }

        internal Token Token
        {
            get { return (Token)_value; }
            set { _value = value; }
        }

        public List<Statement> StatementList
        {
            get { return (List<Statement>)_value; }
            set { _value = value; }
        }

        public List<Expression> ExpressionList
        {
            get { return (List<Expression>)_value; }
            set { _value = value; }
        }
    }
}

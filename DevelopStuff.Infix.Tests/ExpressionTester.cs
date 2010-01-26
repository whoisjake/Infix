using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting;
using Xunit;
using Microsoft.Scripting.Hosting;

namespace DevelopStuff.Infix.Tests
{
    public class ExpressionTester
    {

        private int? Evaluate(string expression)
        {
            ScriptDomainManager manager = ScriptDomainManager.CurrentManager;
            LanguageProvider infix = new InfixLanguageProvider(manager);
            ScriptEngine engine = infix.GetEngine();
            ICompiledCode code = engine.CompileCode(expression);
            return (int?)code.Evaluate();
        }

        [Fact]
        public void AnEmptyExpressionThrowsSyntaxErrorException()
        {
            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("");
            });

            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate(";");
            });
        }

        [Fact]
        public void MissingCharactersExpressionThrowsSyntaxErrorException()
        {
            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("((1 + 1);");
            });
        }

        [Fact]
        public void InvalidCharactersThrowSyntaxErrorException()
        {
            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("(a);");
            });
        }

        [Fact]
        public void ValidExpressionShouldNotThrowSyntaxErrorException()
        {
            Assert.DoesNotThrow(
            delegate
            {
                this.Evaluate("(1);");
            });
        }

        [Fact]
        public void InfixOperatorsShouldBeSupported()
        {
            Assert.Equal<int?>(1, this.Evaluate("(1);"));
            Assert.Equal<int?>(2, this.Evaluate("(1+1);"));
            Assert.Equal<int?>(1, this.Evaluate("(1*1);"));
            Assert.Equal<int?>(1, this.Evaluate("(1/1);"));
            Assert.Equal<int?>(0, this.Evaluate("(1-1);"));

            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("(1%1);");
            });

            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("(1^1);");
            });

            Assert.Throws<SyntaxErrorException>(
            delegate
            {
                this.Evaluate("(1!);");
            });
        }

        [Fact]
        public void InfixExpressionsShouldWork()
        {
            Assert.Equal<int?>(1, this.Evaluate("(1);"));
            Assert.Equal<int?>(2, this.Evaluate("(1+1);"));
            Assert.Equal<int?>(1, this.Evaluate("(1*1);"));
            Assert.Equal<int?>(1, this.Evaluate("(1/1);"));
            Assert.Equal<int?>(0, this.Evaluate("(1-1);"));
            Assert.Equal<int?>(4, this.Evaluate("((1+1)+(1+1));"));
        }
    }
}

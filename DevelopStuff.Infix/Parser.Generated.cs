// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2007
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.1.4.101 (2007-05-31)
// options: lines


using System;
using System.Collections.Generic;
using System.Text;
using gppg;
using DevelopStuff.Infix.Ast;

namespace DevelopStuff.Infix
{
public enum Tokens {error=60,EOF=61,Number=62};

public partial class InfixParser: ShiftReduceParser<InfixValue, LexLocation>
{
  protected override void Initialize()
  {
    this.errToken = (int)Tokens.error;
    this.eofToken = (int)Tokens.EOF;

    states=new State[20];
    AddState(0,new State(new int[]{62,10,40,11},new int[]{-1,1,-5,3,-2,5,-3,19,-4,18}));
    AddState(1,new State(new int[]{61,2}));
    AddState(2,new State(-1));
    AddState(3,new State(new int[]{59,4}));
    AddState(4,new State(-2));
    AddState(5,new State(new int[]{43,6,45,14,59,-3}));
    AddState(6,new State(new int[]{62,10,40,11},new int[]{-3,7,-4,18}));
    AddState(7,new State(new int[]{42,8,47,16,43,-5,45,-5,59,-5,41,-5}));
    AddState(8,new State(new int[]{62,10,40,11},new int[]{-4,9}));
    AddState(9,new State(-8));
    AddState(10,new State(-10));
    AddState(11,new State(new int[]{62,10,40,11},new int[]{-2,12,-3,19,-4,18}));
    AddState(12,new State(new int[]{41,13,43,6,45,14}));
    AddState(13,new State(-11));
    AddState(14,new State(new int[]{62,10,40,11},new int[]{-3,15,-4,18}));
    AddState(15,new State(new int[]{42,8,47,16,43,-6,45,-6,59,-6,41,-6}));
    AddState(16,new State(new int[]{62,10,40,11},new int[]{-4,17}));
    AddState(17,new State(-9));
    AddState(18,new State(-7));
    AddState(19,new State(new int[]{42,8,47,16,43,-4,45,-4,59,-4,41,-4}));

    rules=new Rule[12];
    rules[1]=new Rule(-6, new int[]{-1,61});
    rules[2]=new Rule(-1, new int[]{-5,59});
    rules[3]=new Rule(-5, new int[]{-2});
    rules[4]=new Rule(-2, new int[]{-3});
    rules[5]=new Rule(-2, new int[]{-2,43,-3});
    rules[6]=new Rule(-2, new int[]{-2,45,-3});
    rules[7]=new Rule(-3, new int[]{-4});
    rules[8]=new Rule(-3, new int[]{-3,42,-4});
    rules[9]=new Rule(-3, new int[]{-3,47,-4});
    rules[10]=new Rule(-4, new int[]{62});
    rules[11]=new Rule(-4, new int[]{40,-2,41});

    nonTerminals = new string[] {"", "program", "expression", "term", "factor", 
      "statement", "$accept", };
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 2: // program -> statement ';' 
#line 13 "Parser.y"
			{ _tree = Program.Maek(value_stack.array[value_stack.top-2].Statement); }
        break;
      case 3: // statement -> expression 
#line 15 "Parser.y"
			{ yyval.Statement = ExpressionStatement.Maek(value_stack.array[value_stack.top-1].Expression); }
        break;
      case 4: // expression -> term 
#line 17 "Parser.y"
			{ yyval.Expression = value_stack.array[value_stack.top-1].Expression; }
        break;
      case 5: // expression -> expression '+' term 
#line 18 "Parser.y"
			{ yyval.Expression = Binary.Maek(Operator.Add, value_stack.array[value_stack.top-3].Expression, value_stack.array[value_stack.top-1].Expression); }
        break;
      case 6: // expression -> expression '-' term 
#line 19 "Parser.y"
			{ yyval.Expression = Binary.Maek(Operator.Subtract, value_stack.array[value_stack.top-3].Expression, value_stack.array[value_stack.top-1].Expression); }
        break;
      case 7: // term -> factor 
#line 21 "Parser.y"
			{ yyval.Expression = value_stack.array[value_stack.top-1].Expression; }
        break;
      case 8: // term -> term '*' factor 
#line 22 "Parser.y"
			{ yyval.Expression = Binary.Maek(Operator.Multiply, value_stack.array[value_stack.top-3].Expression, value_stack.array[value_stack.top-1].Expression); }
        break;
      case 9: // term -> term '/' factor 
#line 23 "Parser.y"
			{ yyval.Expression = Binary.Maek(Operator.Divide, value_stack.array[value_stack.top-3].Expression, value_stack.array[value_stack.top-1].Expression); }
        break;
      case 10: // factor -> Number 
#line 25 "Parser.y"
			{ yyval.Expression = Constant.MaekNumbr(value_stack.array[value_stack.top-1].Token); }
        break;
      case 11: // factor -> '(' expression ')' 
#line 26 "Parser.y"
			{ yyval.Expression = value_stack.array[value_stack.top-2].Expression; }
        break;
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (((Tokens)terminal).ToString() != terminal.ToString())
      return ((Tokens)terminal).ToString();
    else
      return CharToString((char)terminal);
  }

#line 28 "Parser.y"

}
}

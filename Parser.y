%using DevelopStuff.Infix.Ast
%namespace DevelopStuff.Infix
%partial
%parsertype InfixParser
%valuetype InfixValue
%start	program

%token<Token> Number
%type<Expression> expression term factor
%type<Statement> statement

%%
program         : statement ';'                 { _tree = Program.Maek($1); }
                ; 
statement       : expression                    { $$ = ExpressionStatement.Maek($1); }
                ;
expression      : term                          { $$ = $1; }
                | expression '+' term           { $$ = Binary.Maek(Operator.Add, $1, $3); }
                | expression '-' term           { $$ = Binary.Maek(Operator.Subtract, $1, $3); }
                ;
term            : factor                        { $$ = $1; }
                | term '*' factor               { $$ = Binary.Maek(Operator.Multiply, $1, $3); }
                | term '/' factor               { $$ = Binary.Maek(Operator.Divide, $1, $3); }
                ;
factor          : Number                        { $$ = Constant.MaekNumbr($1); }
                | '(' expression ')'            { $$ = $2; }   
                ;                                          
%%

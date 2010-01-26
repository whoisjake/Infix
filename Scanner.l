%namespace DevelopStuff.Infix

%%

[0-9]+                          { yylval.Token = Token.Maek(Tokens.Number, yytext, tokLin, tokCol, tokELin, tokECol); return (int)Tokens.Number; }
[\-+*/();]						{ return yytext[0]; }
[.\n]                           { yylval.Token = Token.Maek(Tokens.EOF, tokLin, tokECol, tokELin, tokECol); _eof = true; return (int)Tokens.EOF; }

<<EOF>>                         { _eof = true; }

%%

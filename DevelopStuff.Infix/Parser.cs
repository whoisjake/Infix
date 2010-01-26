using System;
using System.Collections.Generic;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using DevelopStuff.Infix.Ast;

namespace DevelopStuff.Infix
{
    partial class InfixParser
    {

        private Program _tree;

        private static List<Statement> AddStatement(List<Statement> list, Statement statement)
        {
            if (list == null)
            {
                list = new List<Statement>();
            }
            list.Add(statement);
            return list;
        }

        internal Program Parse(CompilerContext context)
        {
            SourceUnit unit = context.SourceUnit;
            Scanner scan = new Scanner();
            string code = unit.GetCode();
            scan.SetSource(code, 0);
            scanner = scan;

            //Trace = true;

            try
            {
                Parse();
                return _tree;
            }
            catch (InfixSyntaxErrorException lse)
            {
                if (unit.Kind == SourceCodeKind.InteractiveCode)
                {
                    if (lse.Eof)
                    {
                        unit.CodeProperties = SourceCodeProperties.IsIncompleteStatement;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return null;
        }
        
        public static object StringToNumber(object obj)
        {
            string str = obj as string;

            if (String.IsNullOrEmpty(str))
            {
                return false;
            }

            switch (str[0])
            {
                case '#':
                    switch (str[1])
                    {
                        case 'b':
                            return StringToNumber(str.Substring(2), 2);
                        case 'o':
                            return StringToNumber(str.Substring(2), 8);
                        case 'd':
                            return StringToNumber(str.Substring(2), 10);
                        case 'x':
                            return StringToNumber(str.Substring(2), 16);
                        default:
                            return false;
                    }
                default:
                    return StringToNumber(obj, 10);
            }
        }

        public static object StringToNumber(object obj, object radix)
        {
            string str = obj as string;

            if (String.IsNullOrEmpty(str))
            {
                return false;
            }

            radix = radix ?? 10;
            int r = (int)radix;

            switch (r)
            {
                case 2:
                    return ParseBinary(str);
                case 8:
                    return ParseOctal(str);
                case 10:
                    int n;
                    if (int.TryParse(str, out n))
                    {
                        return n;
                    }
                    long l;
                    if (long.TryParse(str, out l))
                    {
                        return l;
                    }
                    double d;
                    if (double.TryParse(str, out d))
                    {
                        return d;
                    }
                    decimal dec;
                    if (decimal.TryParse(str, out dec))
                    {
                        return dec;
                    }
                    break;
                case 16:
                    if (str.Length > 16)
                    {
                        return false;
                    }
                    else
                        if (str.Length > 8)
                        {
                            return long.Parse(str, System.Globalization.NumberStyles.HexNumber);
                        }
                        else
                        {
                            return int.Parse(str, System.Globalization.NumberStyles.HexNumber);
                        }
                default:
                    return false;
            }

            return false;
        }

        static object ParseBinary(string str)
        {
            if (str.Length <= 32)
            {
                int b = 1;
                int n = 0;
                for (int i = 0; i < str.Length; i++, b *= 2)
                {
                    char c = str[str.Length - 1 - i];
                    n += b * (c - '0');
                }
                return n;
            }
            else
            {
                long b = 1;
                long n = 0;
                for (int i = 0; i < str.Length; i++, b *= 2)
                {
                    char c = str[str.Length - 1 - i];
                    n += b * (c - '0');
                }
                return n;
            }
        }

        static object ParseOctal(string str)
        {
            if (str.Length < 11) // not precise, bleh
            {
                int b = 1;
                int n = 0;
                for (int i = 0; i < str.Length; i++, b *= 8)
                {
                    char c = str[str.Length - 1 - i];
                    n += b * (c - '0');
                }
                return n;
            }
            else
            {
                long b = 1;
                long n = 0;
                for (int i = 0; i < str.Length; i++, b *= 8)
                {
                    char c = str[str.Length - 1 - i];
                    n += b * (c - '0');
                }
                return n;
            }
        }
    }
}

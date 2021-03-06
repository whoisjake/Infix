//
//  This CSharp output file generated by Gardens Point LEX
//  Version:  0.6.1.183 (2007-09-01)
//  Machine:  MSP-JGOOD
//  DateTime: 11/14/2007 4:02:58 PM
//  UserName: jgood
//  GPLEX input file <Scanner.l>
//  GPLEX frame file <C:\Development\gplex-distro\binaries\gplexx.frame>
//
//  Option settings: parser, minimize, compressnext
//

//
// gplexx.frame
// Version 0.6.0 of 02-July-2007
// Derived from gplex.frame version of 2-September-2006. 
// Left and Right Anchored state support.
// Start condition stack. Two generic params.
// Using fixed length context handling for right anchors
//
using System;
using System.IO;
using System.Collections.Generic;
#if !STANDALONE
using gppg;
#endif


namespace DevelopStuff.Infix
{   
    /// <summary>
    /// Summary Canonical example of GPLEX automaton
    /// </summary>
    
#if STANDALONE
    //
    // These are the dummy declarations for stand-alone GPLEX applications
    // normally these declarations would come from the parser.
    // If you declare /noparser, or %option noparser then you get this.
    //

    public enum Tokens
    { 
      EOF = 0, maxParseToken = int.MaxValue 
      // must have at least these two, values are almost arbitrary
    }

    public abstract class ScanBase
    {
        public abstract int yylex();
#if BABEL
        protected abstract int CurrentSc { get; set; }
        // EolState is the 32-bit of state data persisted at 
        // the end of each line for Visual Studio colorization.  
        // The default is to return CurrentSc.  You must override
        // this if you want more complicated behavior.
        public virtual int EolState { 
            get { return CurrentSc; }
            set { CurrentSc = value; } 
        }
    }
    
    public interface IColorScan
    {
        void SetSource(string source, int offset);
        int GetNext(ref int state, out int start, out int end);
#endif // BABEL
    }

#endif // STANDALONE

    public abstract class ScanBuff
    {
        public const int EOF = -1;
        public abstract int Pos { get; set; }
        public abstract int Read();
        public abstract int Peek();
        public abstract int ReadPos { get; }
        public abstract string GetString(int b, int e);
    }
    
    // If the compiler can't find ScanBase maybe you need to run
    // GPPG with the /gplex option, or GPLEX with /noparser
#if BABEL
    public sealed class Scanner : ScanBase, IColorScan
    {
        public ScanBuff buffer;
        int currentScOrd;  // start condition ordinal
        
        protected override int CurrentSc 
        {
             // The current start state is a property
             // to try to avoid the user error of setting
             // scState but forgetting to update the FSA
             // start state "currentStart"
             //
             get { return currentScOrd; }  // i.e. return YY_START;
             set { currentScOrd = value;   // i.e. BEGIN(value);
                   currentStart = startState[value]; }
        }
#else  // BABEL
    public sealed class Scanner : ScanBase
    {
        public ScanBuff buffer;
        int currentScOrd;  // start condition ordinal
#endif // BABEL
        
        private static int GetMaxParseToken() {
            System.Reflection.FieldInfo f = typeof(Tokens).GetField("maxParseToken");
            return (f == null ? int.MaxValue : (int)f.GetValue(null));
        }
        
        static int parserMax = GetMaxParseToken();
        
        enum Result {accept, noMatch, contextFound};

        const int maxAccept = 3;
        const int initial = 4;
        const int eofNum = 0;
        const int goStart = -1;
        const int INITIAL = 0;

        int state;
        int currentStart = initial;
        int chr;           // last character read
        int cNum;          // ordinal number of chr
        int lNum = 0;      // current line number
        int lineStartNum;  // ordinal number at start of line
        //
        // The following instance variables are used, among other
        // things, for constructing the yylloc location objects.
        //
        int tokPos;        // buffer position at start of token
        int tokNum;        // ordinal number of first character
        int tokLen;        // number of characters in token
        int tokCol;        // zero-based column number at start of token
        int tokLin;        // line number at start of token
        int tokEPos;       // buffer position at end of token
        int tokECol;       // column number at end of token
        int tokELin;       // line number at end of token
        string tokTxt;     // lazily constructed text of token
#if STACK          
        private Stack<int> scStack = new Stack<int>();
#endif // STACK

#region ScannerTables
    struct Table {
        public int min; public int rng; public int dflt;
        public sbyte[] nxt;
        public Table(int m, int x, int d, sbyte[] n) {
            min = m; rng = x; dflt = d; nxt = n;
        }
    };

    static int[] startState = {4, 0};

    static Table[] NxS = new Table[5];

    static Scanner() {
    NxS[0] = new Table(0, 0, 0, null);    NxS[1] = new Table(0, 0, -1, null);    NxS[2] = new Table(0, 0, -1, null);    NxS[3] = new Table(48, 10, -1, new sbyte[] {3, 3, 3, 3, 3, 3, 
          3, 3, 3, 3});
    NxS[4] = new Table(10, 50, -1, new sbyte[] {1, -1, -1, -1, -1, -1, 
          -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
          -1, -1, -1, -1, -1, -1, -1, -1, 2, 2, 2, 2, -1, 2, 1, 2, 
          3, 3, 3, 3, 3, 3, 3, 3, 3, 3, -1, 2});
    }

int NextState(int qStat) {
    if (chr == ScanBuff.EOF)
        return (qStat <= maxAccept && qStat != currentStart ? currentStart : eofNum);
    else {
        int rslt;
        int idx = (byte)(chr - NxS[qStat].min);
        if ((uint)idx >= (uint)NxS[qStat].rng) rslt = NxS[qStat].dflt;
        else rslt = NxS[qStat].nxt[idx];
        return (rslt == goStart ? currentStart : rslt);
    }
}

int NextState() {
    if (chr == ScanBuff.EOF)
        return (state <= maxAccept && state != currentStart ? currentStart : eofNum);
    else {
        int rslt;
        int idx = (byte)(chr - NxS[state].min);
        if ((uint)idx >= (uint)NxS[state].rng) rslt = NxS[state].dflt;
        else rslt = NxS[state].nxt[idx];
        return (rslt == goStart ? currentStart : rslt);
    }
}
#endregion


#if BACKUP
        // ====================== Nested class ==========================

        internal class Context // class used for automaton backup.
        {
            public int bPos;
            public int cNum;
            public int state;
            public int cChr;
        }
#endif // BACKUP


        // ====================== Nested class ==========================

        public sealed class StringBuff : ScanBuff
        {
            string str;        // input buffer
            int bPos;          // current position in buffer
            int sLen;

            public StringBuff(string str)
            {
                this.str = str;
                this.sLen = str.Length;
            }

            public override int Read()
            {
                if (bPos < sLen) return str[bPos++];
#if BABEL
                else if (bPos == sLen) { bPos++; return '\n'; }   // one strike, see newline
#endif // BABEL
                else { bPos++; return EOF; }                      // two strikes and you're out!
            }
            
            public override int ReadPos { get { return bPos - 1; } }

            public override int Peek()
            {
                if (bPos < sLen) return str[bPos];
                else return '\n';
            }

            public override string GetString(int beg, int end)
            {
                //  "end" can be greater than sLen with the BABEL
                //  option set.  Read returns a "virtual" EOL if
                //  an attempt is made to read past the end of the
                //  string buffer.  Without the guard any attempt 
                //  to fetch yytext for a token that includes the 
                //  EOL will throw an index exception.
                if (end > sLen) end = sLen;
                if (end <= beg) return ""; 
                else return str.Substring(beg, end - beg);
            }

            public override int Pos
            {
                get { return bPos; }
                set { bPos = value; }
            }
        }

        // ====================== Nested class ==========================

        public sealed class StreamBuff : ScanBuff
        {
            BufferedStream bStrm;   // input buffer
            int delta = 1;          // number of bytes in chr, could be 0 for EOF.

            public StreamBuff(Stream str) { this.bStrm = new BufferedStream(str); }

            public override int Read() {
                int ch0 = bStrm.ReadByte();
                delta = (ch0 == EOF ? 0 : 1);
                return ch0; 
            }
            
            public override int ReadPos {
                get { return (int)bStrm.Position - delta; }
            }

            public override int Peek()
            {
                int rslt = bStrm.ReadByte();
                bStrm.Seek(-delta, SeekOrigin.Current);
                return rslt;
            }

            public override string GetString(int beg, int end)
            {
                if (end - beg <= 0) return "";
                long savePos = bStrm.Position;
                char[] arr = new char[end - beg];
                bStrm.Position = (long)beg;
                for (int i = 0; i < (end - beg); i++)
                    arr[i] = (char)bStrm.ReadByte();
                bStrm.Position = savePos;
                return new String(arr);
            }

            // Pos is the position *after* reading chr!
            public override int Pos
            {
                get { return (int)bStrm.Position; }
                set { bStrm.Position = value; }
            }
        }

        // ====================== Nested class ==========================

        /// <summary>
        /// This is the Buffer for UTF8 files.
        /// It attempts to read the encoding preamble, which for 
        /// this encoding should be unicode point \uFEFF which is 
        /// encoded as EF BB BF
        /// </summary>
        public class TextBuff : ScanBuff
        {
            protected BufferedStream bStrm;   // input buffer
            protected int delta = 1;          // length of chr, zero for EOF!
            
            private Exception BadUTF8()
            { return new Exception(String.Format("BadUTF8 Character")); }

            /// <summary>
            /// TextBuff factory.  Reads the file preamble
            /// and returns a TextBuff, LittleEndTextBuff or
            /// BigEndTextBuff according to the result.
            /// </summary>
            /// <param name="strm">The underlying stream</param>
            /// <returns></returns>
            public static TextBuff NewTextBuff(Stream strm)
            {
                // First check if this is a UTF16 file
                //
                int b0 = strm.ReadByte();
                int b1 = strm.ReadByte();

                if (b0 == 0xfe && b1 == 0xff)
                    return new BigEndTextBuff(strm);
                if (b0 == 0xff && b1 == 0xfe)
                    return new LittleEndTextBuff(strm);
                
                int b2 = strm.ReadByte();
                if (b0 == 0xef && b1 == 0xbb && b2 == 0xbf)
                    return new TextBuff(strm);
                //
                // There is no unicode preamble, so we
                // must go back to the UTF8 default.
                //
                strm.Seek(0, SeekOrigin.Begin);
                return new TextBuff(strm);
            }

            protected TextBuff(Stream str) { 
                this.bStrm = new BufferedStream(str);
            }

            public override int Read()
            {
                int ch0 = bStrm.ReadByte();
                int ch1;
                int ch2;
                if (ch0 < 0x7f)
                {
                    delta = (ch0 == EOF ? 0 : 1);
                    return ch0;
                }
                else if ((ch0 & 0xe0) == 0xc0)
                {
                    delta = 2;
                    ch1 = bStrm.ReadByte();
                    if ((ch1 & 0xc0) == 0x80)
                        return ((ch0 & 0x1f) << 6) + (ch1 & 0x3f);
                    else
                        throw BadUTF8();
                }
                else if ((ch0 & 0xf0) == 0xe0)
                {
                    delta = 3;
                    ch1 = bStrm.ReadByte();
                    ch2 = bStrm.ReadByte();
                    if ((ch1 & ch2 & 0xc0) == 0x80)
                        return ((ch0 & 0xf) << 12) + ((ch1 & 0x3f) << 6) + (ch2 & 0x3f);
                    else
                        throw BadUTF8();
                }
                else
                    throw BadUTF8();
            }

            public sealed override int ReadPos
            {
                get { return (int)bStrm.Position - delta; }
            }

            public sealed override int Peek()
            {
                int rslt = Read();
                bStrm.Seek(-delta, SeekOrigin.Current);
                return rslt;
            }

            /// <summary>
            /// Returns the string from the buffer between
            /// the given file positions.  This needs to be
            /// done carefully, as the number of characters
            /// is, in general, not equal to (end - beg).
            /// </summary>
            /// <param name="beg">Begin filepos</param>
            /// <param name="end">End filepos</param>
            /// <returns></returns>
            public sealed override string GetString(int beg, int end)
            {
                int i;
                if (end - beg <= 0) return "";
                long savePos = bStrm.Position;
                char[] arr = new char[end - beg];
                bStrm.Position = (long)beg;
                for (i = 0; bStrm.Position < end; i++)
                    arr[i] = (char)Read();
                bStrm.Position = savePos;
                return new String(arr, 0, i);
            }

            // Pos is the position *after* reading chr!
            public sealed override int Pos
            {
                get { return (int)bStrm.Position; }
                set { bStrm.Position = value; }
            }
        }

        // ====================== Nested class ==========================
        /// <summary>
        /// This is the Buffer for Big-endian UTF16 files.
        /// </summary>
        public sealed class BigEndTextBuff : TextBuff
        {
            internal BigEndTextBuff(Stream str) : base(str) { } // 

            public override int Read()
            {
                int ch0 = bStrm.ReadByte();
                int ch1 = bStrm.ReadByte();
                if (ch1 == EOF)
                {
                    // An EOF in either byte counts as an EOF
                    delta = (ch0 == EOF ? 0 : 1);
                    return -1;
                }
                else
                {
                    delta = 2;
                    return (ch0 << 8) + ch1;
                }
            }
        }
        
        // ====================== Nested class ==========================
        /// <summary>
        /// This is the Buffer for Little-endian UTF16 files.
        /// </summary>
        public sealed class LittleEndTextBuff : TextBuff
        {
            internal LittleEndTextBuff(Stream str) : base(str) { } // { this.bStrm = new BufferedStream(str); }

            public override int Read()
            {
                int ch0 = bStrm.ReadByte();
                int ch1 = bStrm.ReadByte();
                if (ch1 == EOF)
                {
                    // An EOF in either byte counts as an EOF
                    delta = (ch0 == EOF ? 0 : 1);
                    return -1;
                }
                else
                {
                    delta = 2;
                    return (ch1 << 8) + ch1;
                }
            }
        }
        // =================== End Nested classes =======================

        public Scanner(Stream file) {
            buffer = new StreamBuff(file);
            this.cNum = -1;
            this.chr = '\n'; // to initialize yyline, yycol and lineStart
            GetChr();
        }

        public Scanner() { }

        void GetChr()
        {
            if (chr == '\n') 
            { 
                lineStartNum = cNum + 1; 
                lNum++; 
            }
            chr = buffer.Read();
            cNum++;
        }

        void MarkToken()
        {
            tokPos = buffer.ReadPos;
            tokNum = cNum;
            tokLin = lNum;
            tokCol = cNum - lineStartNum;
        }
        
        void MarkEnd()
        {
            tokTxt = null;
            tokLen = cNum - tokNum;
            tokEPos = buffer.ReadPos;
            tokELin = lNum;
            tokECol = cNum - lineStartNum;
        }

        // ================ StringBuffer Initialization ===================

        public void SetSource(string source, int offset)
        {
            this.buffer = new StringBuff(source);
            this.buffer.Pos = offset;
            this.cNum = offset - 1;
            this.chr = '\n'; // to initialize yyline, yycol and lineStart
            GetChr();
        }
        
#if BABEL
        //
        //  Get the next token for Visual Studio
        //
        //  "state" is the inout mode variable that maintains scanner
        //  state between calls, using the EolState property. In principle,
        //  if the calls of EolState are costly set could be called once
        //  only per line, at the start; and get called only at the end
        //  of the line. This needs more infrastructure ...
        //
        public int GetNext(ref int state, out int start, out int end)
        {
            Tokens next;
            int s, e;
            s = state;        // state at start
            EolState = state;
            next = (Tokens)Scan();
            state = EolState;
            e = state;       // state at end;
            start = tokPos;
            end = tokEPos - 1; // end is the index of last char.
            return (int)next;
        }        
#endif // BABEL

        // ======== IScanner<> Implementation =========

        public override int yylex()
        {
            // parserMax is set by reflecting on the Tokens
            // enumeration.  If maxParseTokeen is defined
            // that is used, otherwise int.MaxValue is used.
            int next;
            do { next = Scan(); } while (next >= parserMax);
            return next;
        }
        
        int yyleng { get { return tokLen; } }
        int yypos { get { return tokPos; } }
        int yyline { get { return tokLin; } }
        int yycol { get { return tokCol; } }

        public string yytext
        {
            get 
            {
                if (tokTxt == null) 
                    tokTxt = buffer.GetString(tokPos, tokEPos);
                return tokTxt;
            }
        }

        void yyless(int n) { 
            buffer.Pos = tokPos;
            // Must read at least one char, so set before start.
            cNum = tokNum - 1;
            for (int i = 0; i <= n; i++) GetChr();
            MarkEnd();
        }

        // ============ methods available in actions ==============

        internal int YY_START {
            get { return currentScOrd; }
            set { currentScOrd = value; } 
        }
        
        internal void BEGIN(int next) {
            currentScOrd = next;
            currentStart = startState[next];
        }

        // ============== The main tokenizer code =================

        int Scan()
        {
                for (; ; )
                {
                    int next;              // next state to enter                   
#if BACKUP
                    bool inAccept = false; // inAccept ==> current state is an accept state
                    Result rslt = Result.noMatch;
                    // skip "idle" transitions
#if LEFTANCHORS
                    if (lineStartNum == cNum && NextState(anchorState[currentScOrd]) != currentStart)
                        state = anchorState[currentScOrd];
                    else {
                        state = currentStart;
                        while (NextState() == state) {
                            GetChr();
                            if (lineStartNum == cNum) {
                                int anchor = anchorState[currentScOrd];
                                if (NextState(anchor) != state) {
                                    state = anchor; 
                                    break;
                                }
                            }
                        }
                    }
#else // !LEFTANCHORS
                    state = currentStart;
                    while (NextState() == state) 
                        GetChr(); // skip "idle" transitions
#endif // LEFTANCHORS
                    MarkToken();
                    
                    while ((next = NextState()) != currentStart)
                        if (inAccept && next > maxAccept) // need to prepare backup data
                        {
                            Context ctx = new Context();
                            rslt = Recurse2(ctx, next);
                            if (rslt == Result.noMatch) RestoreStateAndPos(ctx);
                            break;
                        }
                        else
                        {
                            state = next;
                            GetChr();
                            if (state <= maxAccept) inAccept = true;
                        }
#else // !BACKUP
#if LEFTANCHORS
                    if (lineStartNum == cNum) {
                        int anchor = anchorState[currentScOrd];
                        if (NextState(anchor) != currentStart)
                            state = anchor;
                    }
                    else {
                        state = currentStart;
                        while (NextState() == state) {
                            GetChr();
                            if (lineStartNum == cNum) {
                                anchor = anchorState[currentScOrd];
                                if (NextState(anchor) != state) {
                                    state = anchor;
                                    break;
                                }
                            }
                        }
                    }
#else // !LEFTANCHORS
                    state = currentStart;
                    while (NextState() == state) 
                        GetChr(); // skip "idle" transitions
#endif // LEFTANCHORS
                    MarkToken();
                    // common code
                    while ((next = NextState()) != currentStart)
                    {
                        state = next;
                        GetChr();
                    }
#endif // BACKUP
                    if (state > maxAccept) 
                        state = currentStart;
                    else
                    {
                        MarkEnd();
#region ActionSwitch
#pragma warning disable 162
    switch (state)
    {
        case eofNum:
            switch (currentStart) {
                case 4:
_eof = true;
                    break;
            }
            return (int)Tokens.EOF;
        case 1:
yylval.Token = Token.Maek(Tokens.EOF, tokLin, tokECol, tokELin, tokECol); _eof = true; return (int)Tokens.EOF;
            break;
        case 2:
return yytext[0];
            break;
        case 3:
yylval.Token = Token.Maek(Tokens.Number, yytext, tokLin, tokCol, tokELin, tokECol); return (int)Tokens.Number;
            break;
        default:
            break;
    }
#pragma warning restore 162
#endregion
                    }
                }
        }

#if BACKUP
        Result Recurse2(Context ctx, int next)
        {
            // Assert: at entry "state" is an accept state AND
            //         NextState(state, chr) != currentStart AND
            //         NextState(state, chr) is not an accept state.
            //
            bool inAccept;
            SaveStateAndPos(ctx);
            state = next;
            if (state == eofNum) return Result.accept;
            GetChr();
            inAccept = false;

            while ((next = NextState()) != currentStart)
            {
                if (inAccept && next > maxAccept) // need to prepare backup data
                    SaveStateAndPos(ctx);
                state = next;
                if (state == eofNum) return Result.accept;
                GetChr(); 
                inAccept = (state <= maxAccept);
            }
            if (inAccept) return Result.accept; else return Result.noMatch;
        }

        void SaveStateAndPos(Context ctx)
        {
            ctx.bPos  = buffer.Pos;
            ctx.cNum  = cNum;
            ctx.state = state;
            ctx.cChr = chr;
        }

        void RestoreStateAndPos(Context ctx)
        {
            buffer.Pos = ctx.bPos;
            cNum = ctx.cNum;
            state = ctx.state;
            chr = ctx.cChr;
        }

        void RestorePos(Context ctx) { buffer.Pos = ctx.bPos; cNum = ctx.cNum; }
#endif // BACKUP

        // ============= End of the tokenizer code ================

#if STACK        
        internal void yy_clear_stack() { scStack.Clear(); }
        internal int yy_top_state() { return scStack.Peek(); }
        
        internal void yy_push_state(int state)
        {
            scStack.Push(currentScOrd);
            BEGIN(state);
        }
        
        internal void yy_pop_state()
        {
            // Protect against input errors that pop too far ...
            if (scStack.Count > 0) {
				int newSc = scStack.Pop();
				BEGIN(newSc);
            } // Otherwise leave stack unchanged.
        }
 #endif // STACK

        internal void ECHO() { Console.Out.Write(yytext); }
        
    } // end class Scanner
} // end namespace

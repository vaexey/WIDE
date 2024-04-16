using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIDE.View
{
    internal static class AssemblerEditorInitializer
    {
        internal static void Init(Scintilla s)
        {
            s.Dock = DockStyle.Fill;

            s.WrapMode = WrapMode.None;
            s.IndentationGuides = IndentView.LookBoth;

            Colors(s);
            Syntax(s);
            //Folding(s);
        }

        private static void Colors(Scintilla s)
        {
            s.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        private static void Hotkeys(Scintilla s)
        {

        }

        private static void Syntax(Scintilla s)
        {
            // Configure the default style
            s.StyleResetDefault();
            s.Styles[Style.Default].Font = "Consolas";
            s.Styles[Style.Default].Size = 10;
            s.Styles[Style.Default].BackColor = IntToColor(0xffffff);
            s.Styles[Style.Default].ForeColor = IntToColor(0x000000);
            s.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            //s.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0x111111);
            //s.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            //s.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            //s.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            //s.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            //s.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            //s.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            //s.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            //s.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            //s.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            //s.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            //s.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            //s.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            //s.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            //s.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            //s.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);
            s.Styles[Style.Asm.Number].ForeColor = IntToColor(0x946d03);
            s.Styles[Style.Asm.Identifier].ForeColor = IntToColor(0x573619);
            s.Styles[Style.Asm.Comment].ForeColor = IntToColor(0x1a7007);

            s.Lexer = Lexer.Asm;

            //s.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            //s.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

            s.SetKeywords(0, "mov");
        }

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;
        private static void Folding(Scintilla s)
        {

            s.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            s.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            s.SetProperty("fold", "1");
            s.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            s.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            s.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            s.Margins[FOLDING_MARGIN].Sensitive = true;
            s.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                s.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                s.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            s.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            s.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            s.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            s.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            s.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            s.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            s.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            s.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        internal static Color HexToColor(string hex)
        {
            return ColorTranslator.FromHtml($"#{hex}");
        }
    }
}

using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Controls
{
    internal static class HexEditorInitializer
    {
        internal static void Init(HexBox hb)
        {
            hb.ColumnInfoVisible = true;
            hb.HexCasing = HexCasing.Lower;
            hb.LineInfoVisible = true;
            hb.ShadowSelectionColor = Color.FromArgb(100, 60, 188, 255);
            hb.StringViewVisible = true;
            hb.UseFixedBytesPerLine = false;
            hb.VScrollBarVisible = true;


            hb.InfoForeColor = Color.Gray;
            hb.BackColor = Styles.ColorBackground;

            //hb.ByteProvider = new DynamicByteProvider(
            //    Encoding.ASCII.GetBytes("FRANZL LANG"));
        }
    }
}

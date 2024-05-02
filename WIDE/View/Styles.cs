using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View
{
    public static class Styles
    {
        public static Font FontMonospace(float emSize) => new("Courier New", emSize);
        public static Font FontSans(float emSize) => new("Courier New", emSize);

        public static Color ColorFont { get; } = Color.FromArgb(200,200,200);
        public static Color ColorBackground { get; } = Color.FromArgb(50, 50, 50);

        public static Color ColorInner { get; } = Color.FromArgb(20, 20, 20);

        public static Color ColorCollapseBar { get; } = Mix(ColorBackground, 1, 1, 1.5);


        private static Color Mix(Color basic, double rMod, double gMod, double bMod)
        {
            return Color.FromArgb(
                    (byte)Math.Clamp(rMod * basic.R, 0, 255),
                    (byte)Math.Clamp(gMod * basic.G, 0, 255),
                    (byte)Math.Clamp(bMod * basic.B, 0, 255)
                );
        }
    }
}

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
        public static Font FontSans(float emSize) => new("Roboto", emSize);

        public static Color ColorBackground { get; } = ColorOf(50, 50, 50); // ColorOf(200,200,200);
        public static Color ColorFont { get; } = Grayscale(Invert(ColorBackground));
        public static Color ColorSelection { get; } = ColorOf(200, 50, 50);

        public static Color ColorInner { get; } = Mix(ColorBackground, 0.6, 0.6, 0.6);

        public static Color ColorCollapseBar { get; } = Mix(ColorBackground, 1, 1, 1.5);

        private static Color ColorOf(double r, double g, double b)
        {
            return Color.FromArgb(
                    (byte)Math.Clamp(r, 0, 255),
                    (byte)Math.Clamp(g, 0, 255),
                    (byte)Math.Clamp(b, 0, 255)
                );
        }
        private static Color Mix(Color basic, double rMod, double gMod, double bMod)
        {
            var qvg = (basic.R + basic.G + basic.B) / 3.0;

            if(qvg > 128)
            {
                rMod = 1 / rMod;
                gMod = 1 / gMod;
                bMod = 1 / bMod;
            }

            return ColorOf(
                rMod * basic.R,
                gMod * basic.G,
                bMod * basic.B
                );
        }
        private static Color Grayscale(Color basic)
        {
            var qvg = (basic.R + basic.G + basic.B) / 3.0;
            return ColorOf(qvg, qvg, qvg);
        }
        private static Color Invert(Color basic, double rMod = 1, double gMod = 1, double bMod = 1)
        {
            return ColorOf(
                rMod * (255 - basic.R),
                gMod * (255 - basic.G),
                bMod * (255 - basic.B)
            );
        }
    }
}

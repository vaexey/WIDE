using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BlockMetadata
    {
        public bool Hidden { get; set; } = true;

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;

        public string Title { get; set; } = "Unnamed";

        public BlockMetadata() { }

        public BlockMetadata(string title, int x, int y, int width = 100, int? height = null)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height ?? width;
            Title = title;
            Hidden = false;
        }

        public override string ToString()
        {
            return string.Format("{0}, ({1}, {2})", Title, X, Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using static WIDEToolkit.Emulator.Blocks.ALUBlock;

namespace WIDEToolkit.Emulator.Flow
{
    public class ALUSignal : Signal
    {
        public string Endpoint1 { get; }
        public string Endpoint2 { get; }
        public string Endpoint3 { get; }

        public ALUOperation Operation { get; }
        public int Width { get; }

        public ALUSignal(string name, string endpoint1, string endpoint2, string endpoint3, ArchBlock owner, ALUOperation operation, int width) : base(name, owner)
        {
            Endpoint1 = endpoint1;
            Endpoint2 = endpoint2;
            Endpoint3 = endpoint3;
            Operation = operation;
            Width = width;
        }
    }
}

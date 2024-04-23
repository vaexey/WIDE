using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class ALUSignal : Signal
    {
        public string[] SourceEndpoints;
        public string[] DestEndpoints;

        public ALUOperation Operation;

        public ALUSignal(string name, ArchBlock owner, string[] sourceEndpoints, string[] destEndpoints, ALUOperation operation)
            : base(name, owner)
        {
            SourceEndpoints = sourceEndpoints;
            DestEndpoints = destEndpoints;
            Operation = operation;
        }
    }
}

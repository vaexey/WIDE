using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public abstract class ALUOperation
    {
        public abstract void Execute(Endpoint[] sources, Endpoint[] destinations);
    }
}

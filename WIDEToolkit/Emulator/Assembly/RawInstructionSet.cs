using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Assembly
{
    public class RawInstructionSet
    {
        public RawInstruction[] Instructions = Array.Empty<RawInstruction>();

        public RawInstruction? ParseInstruction(WORD ir)
        {
            foreach(var i in Instructions)
            {
                var opc = i.OpCode;

                if (opc == ir.Slice(0, opc.Width))
                    return i;
            }

            return null;
        }
    }
}

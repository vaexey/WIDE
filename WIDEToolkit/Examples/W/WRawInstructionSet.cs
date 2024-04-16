using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Assembly;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Examples.W
{
    public class WRawInstructionSet : RawInstructionSet
    {
        public WRawInstructionSet(Architecture arch, int opcodeWidth = 3)
        {
            Instructions = new RawInstruction[]
            {
                //new(
                //    WORD.FromUInt64(0ul, opcodeWidth),
                //    "")
            };
        }
    }
}

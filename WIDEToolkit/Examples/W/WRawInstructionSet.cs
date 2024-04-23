using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Assembly;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Examples.W
{
    public class WRawInstructionSet : RawInstructionSet
    {
        public WRawInstructionSet(Architecture arch, int opcodeWidth = 3)
        {
            Instructions = new RawInstruction[]
            {
                new(
                    WORD.FromUInt64(0ul, opcodeWidth),
                    "DOD",
                    new Signal[][][]
                    {
                        new Signal[][]
                        {
                            new Signal[]
                            {
                                arch.GetSignal("weja"),
                                arch.GetSignal("dod"),
                                arch.GetSignal("weak")
                            }
                        }
                    }
                )
            };
        }
    }
}

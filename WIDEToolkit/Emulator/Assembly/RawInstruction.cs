using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Assembly
{
    public class RawInstruction
    {
        public WORD OpCode { get; }
        public string Name { get; }
        public Signal[][] Cycles { get; }

        public RawInstruction(WORD opcode, string name, Signal[][] cycles)
        {
            OpCode = opcode;
            Name = name;
            Cycles = cycles;
        }

        public RawInstruction(Architecture arch, WORD opcode, string name, string[] cycles)
            : this(
                  opcode, 
                  name, 
                  cycles.Select(x => x.Split(" ").Select(s => arch.GetSignal(s)).ToArray()).ToArray()
              )
        { }
    }
}

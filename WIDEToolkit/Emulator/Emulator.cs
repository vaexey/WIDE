using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Assembly;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator
{
    public class Emulator
    {
        public Architecture Arch { get; }
        public RawInstructionSet Set { get; }

        public RawInstruction? CurrentInstruction { get; set; } = null;
        public int CycleIndex { get; set; } = 0;
        public int ForkValue { get; set; } = 0;

        public Emulator(Architecture arch, RawInstructionSet set)
        {
            Arch = arch;
            Set = set;
        }

        public void Cycle()
        {
            if (CurrentInstruction != null &&
                CycleIndex >= CurrentInstruction.Cycles.Length)
                CurrentInstruction = null;

            if(CurrentInstruction == null)
            {
                Fetch();
                return;
            }

            DoSignals();
        }

        protected void Fetch()
        {
            // TODO: instr reg size
            var ir = Arch.GetEndpoint(Arch.InstructionEndpoint).ReadEndpoint(64);

            var instr = Set.ParseInstruction(ir);

            if (instr == null)
                throw new CycleException($"Unknown instruction opcode 0x{ir.ToString(16)}");

            CycleIndex = 0;
            CurrentInstruction = instr;
        }

        protected void DoSignals()
        {
            ForkValue = CurrentInstruction.Forks.GetForkValue(Arch);

            var sigs = CurrentInstruction.Cycles[CycleIndex];

            //Arch.ExecSignals(sigs);

            CycleIndex++;
        }
    }
}

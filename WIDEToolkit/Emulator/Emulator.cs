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

        public int FetchCycleIndex { get; set; } = 1;
        public bool RefetchAfterInstruction { get; set; } = false;

        public Emulator(Architecture arch, RawInstructionSet set)
        {
            Arch = arch;
            Set = set;
        }

        public void Cycle()
        {
            //if (CurrentInstruction != null &&
            //    CycleIndex >= CurrentInstruction.Cycles.Length)
            //    CurrentInstruction = null;
            //if(CurrentInstruction != null)
            //{
            //    if(CycleIndex >= CurrentInstruction.Cycles.Length)
            //    {
            //        CurrentInstruction = null;
            //    }
            //    else if (CurrentInstruction.Cycles[CycleIndex][ForkValue].Length == 0)
            //    {
            //        CurrentInstruction = null;
            //    }
            //}

            //if(CurrentInstruction == null)
            //{
            //    Fetch();
            //    return;
            //}

            //DoSignals();

            //if (CurrentInstruction != null)
            //{
            //    if (CycleIndex >= CurrentInstruction.Cycles.Length
            //        || CurrentInstruction.Cycles[CycleIndex][ForkValue].Length == 0)
            //    {
            //        CycleIndex = 0;
            //    }
            //}

            //if(CycleIndex == FetchCycleIndex)
            //{
            //    Fetch();
            //}

            //if (CurrentInstruction != null)
            //    DoSignals();
            //else
            //    Fetch();

            if (CurrentInstruction != null)
            {
                if (CycleIndex >= CurrentInstruction.Cycles.Length
                    || CurrentInstruction.Cycles[CycleIndex][ForkValue].Length == 0)
                {
                    if(RefetchAfterInstruction)
                    {
                        CurrentInstruction = null;
                    }

                    CycleIndex = 0;

                    //return;
                }
            }

            if (CurrentInstruction == null)
                Fetch();
            else if (CycleIndex == FetchCycleIndex)
                Fetch();

            DoSignals();
        }

        protected void Fetch()
        {
            // TODO: instr reg size
            var ir = Arch.GetEndpoint(Arch.InstructionEndpoint).ReadEndpoint(64);

            var instr = Set.ParseInstruction(ir);

            if (instr == null)
                throw new CycleException($"Unknown instruction opcode 0x{ir.ToString(16)}");
            //if(instr == null)
            //{
            //    // todo info
            //}

            //CycleIndex = 0;
            CurrentInstruction = instr;
        }

        protected void DoSignals()
        {
            ForkValue = CurrentInstruction.Forks.GetForkValue(Arch);

            var sigs = CurrentInstruction.Cycles[CycleIndex];

            Arch.ExecSignals(sigs[ForkValue]);
            Arch.Commit();

            CycleIndex++;
        }
    }
}

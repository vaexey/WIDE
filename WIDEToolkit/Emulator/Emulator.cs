using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Emulator.Assembly;

namespace WIDEToolkit.Emulator
{
    public class Emulator
    {
        public Architecture Arch { get; }
        public RawInstructionSet Set { get; set; }

        public RawInstruction? CurrentInstruction { get; set; } = null;
        public int CycleIndex { get; set; } = 0;
        public int ForkValue { get; set; } = 0;

        public int FetchCycleIndex { get; set; } = 1;
        public bool RefetchAfterInstruction { get; set; } = false;

        public double CPS => Paused ? 0 : cycleCount / DateTime.Now.Subtract(cycleCountStart).TotalSeconds;
        protected DateTime cycleCountStart = DateTime.Now;
        protected int cycleCount = 0;

        public bool Paused { get; protected set; } = true;
        public EmulatorPauseReason PauseReason { get; protected set; } = EmulatorPauseReason.UNKNOWN;
        public bool PauseOnCycle { get; set; } = false;
        public bool PauseOnInstruction { get; set; } = false;

        public EmulatorLoopMode Mode { get; set; } = EmulatorLoopMode.CYCLE;

        public Emulator(Architecture arch, RawInstructionSet set)
        {
            Arch = arch;
            Set = set;
        }

        public void Loop()
        {
            if (Paused)
                return;

            switch(Mode)
            {
                case EmulatorLoopMode.CYCLE:
                    Cycle();
                    break;
                case EmulatorLoopMode.INSTRUCTION:
                    Instruction();
                    break;
            }
        }
        
        public void Pause(EmulatorPauseReason reason)
        {
            Paused = true;
            PauseReason = reason;
            cycleCount = 0;
        }

        public void Unpause()
        {
            Paused = false;
            PauseReason = EmulatorPauseReason.UNKNOWN;
        } 

        public void Reset()
        {
            CycleIndex = 0;
            CurrentInstruction = null;
        }

        public void Instruction()
        {
            var p = Paused;
            var poi = PauseOnInstruction;
            var poc = PauseOnCycle;

            Paused = false;
            PauseOnCycle = false;
            PauseOnInstruction = true;

            while (!Paused)
                Cycle();

            PauseOnInstruction = poi;
            PauseOnCycle = poc;

            

            Paused = p;

            if (PauseOnInstruction)
                Pause(EmulatorPauseReason.INSTRUCTION);
        }

        public void Cycle()
        {
            if (cycleCount == 0)
                cycleCountStart = DateTime.Now;
            cycleCount++;

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

                    if (PauseOnInstruction)
                        Pause(EmulatorPauseReason.INSTRUCTION);
                }
            }

            if (CurrentInstruction == null)
                Fetch();
            else if (CycleIndex == FetchCycleIndex)
                Fetch();

            DoSignals();

            if (PauseOnCycle)
                Pause(EmulatorPauseReason.CYCLE);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Fragment
{
    public class ConstFragment : AsmInstructionFragment
    {
        public WORD Value { get; }

        public ConstFragment(WORD value)
        {
            Value = value;
        }

        public override WORD Build(AsmImplementedInstruction instr) => Value.Clone();
        public override int CalculateWidth(AsmImplementedInstruction instr) => Value.Width;
    }
}

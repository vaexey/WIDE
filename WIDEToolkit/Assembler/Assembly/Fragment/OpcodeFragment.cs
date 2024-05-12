using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Fragment
{
    public class OpcodeFragment : AsmInstructionFragment
    {
        public override WORD Build(AsmImplementedInstruction instr)
        {
            return instr.Parent.Parent.Opcode.Clone();
        }

        public override int CalculateWidth(AsmImplementedInstruction instr)
        {
            return instr.Parent.Parent.Opcode.Width;
        }
    }
}

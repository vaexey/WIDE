using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Fragment
{
    public abstract class AsmInstructionFragment
    {
        public abstract WORD Build(AsmImplementedInstruction instr);

        public abstract int CalculateWidth(AsmImplementedInstruction instr);
    }
}

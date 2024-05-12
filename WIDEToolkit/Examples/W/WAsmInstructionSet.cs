using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly;
using WIDEToolkit.Assembler.Assembly.Fragment;
using WIDEToolkit.Assembler.Assembly.Operand;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Examples.W
{
    public class WAsmInstructionSet : AsmInstructionSet
    {
        public WAsmInstructionSet()
        {
            Instructions = new()
            {
                Make0Arg("STP", 0ul),

                Make1Arg("DOD", 1ul),
                Make1Arg("ODE", 2ul),
                Make1Arg("POB", 3ul),
                Make1Arg("LAD", 4ul),
                Make1Arg("SOB", 5ul),

                Make1Arg("RST", 0ul),
                Make0Arg("RPA", 0ul),
            };
        }

        private AsmInstruction Make0Arg(string name, ulong opcode)
        {
            return AsmInstruction.Create(name, WORD.FromUInt64(opcode, 3))
                .WithImplementation()
                .Operand(new StringOperand(name))
                .Fragment(new ConstFragment(WORD.FromUInt64(0, 5)))
                .Fragment(new OpcodeFragment())
                .Parent;
        }

        private AsmInstruction Make1Arg(string name, ulong opcode)
        {
            return AsmInstruction.Create(name, WORD.FromUInt64(opcode, 3))
                .WithImplementation()
                .Operand(new StringOperand(name))
                .Operand(new ValueOperand())
                .Fragment(new ParamFragment(0, 5))
                .Fragment(new OpcodeFragment())
                .Parent;
        }
    }
}

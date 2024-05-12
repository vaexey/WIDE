using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;

namespace WIDEToolkit.Assembler.Assembly.Operand
{
    public class ImmediateOperand : AsmInstructionOperand<ImmediateSymbol>
    {
        public override IAsmSymbol[] GenerateParams(IAsmSymbol source)
        {
            return new[]
            {
                source
            };
        }
    }
}

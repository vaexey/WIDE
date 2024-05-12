using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;

namespace WIDEToolkit.Assembler.Assembly
{
    public class AsmInstructionSet
    {
        public List<AsmInstruction> Instructions { get; set; } = new();

        public AsmImplementedInstruction? ParseInstruction(List<IAsmSymbol> symbols)
        {
            foreach (var instr in Instructions)
            {
                var result = instr.TryMatch(symbols);

                if (result is not null)
                    return result;
            }

            return null;
        }
    }
}

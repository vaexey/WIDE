using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Fragment;
using WIDEToolkit.Assembler.Assembly.Operand;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly
{
    public class AsmInstructionImplementation
    {
        public AsmInstruction Parent { get; }
        public List<AsmInstructionOperand> Operands { get; set; } = new();
        public List<AsmInstructionFragment> Fragments { get; set; } = new();

        public AsmInstructionImplementation(AsmInstruction parent) { Parent = parent; }

        public AsmImplementedInstruction? TryMatch(List<IAsmSymbol> symbols)
        {
            if (symbols.Count != Operands.Count)
                return null;

            AsmImplementedInstruction aii = new(this);

            for (int i = 0; i < Operands.Count; i++)
            {
                var operand = Operands[i];
                var symbol = symbols[i];

                if(!operand.Match(symbol))
                {
                    return null;
                }

                aii.Origin.Add(symbol);
                //var param = operand.GenerateParams(symbol);

                //if (param.Length > 0)
                //    aii.Params.AddRange(param);
            }

            aii.Width = Fragments.Select(f => f.CalculateWidth(aii)).Sum();

            return aii;
        }
        
        public AsmInstructionImplementation WithImplementation()
        {
            return Parent.WithImplementation();
        }

        public AsmInstructionImplementation Operand(AsmInstructionOperand operand)
        {
            Operands.Add(operand);

            return this;
        }
        
        public AsmInstructionImplementation Fragment(AsmInstructionFragment fragment)
        {
            Fragments.Add(fragment);

            return this;
        }
    }
}

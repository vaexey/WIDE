using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler.Assembly.Operand
{
    public class ValueOperand : AsmInstructionOperand
    {
        public override bool Match(IAsmSymbol symbol)
        {
            return (symbol is ImmediateSymbol)
                || (symbol is LabelSymbol)
                || (symbol is TranslatedLabelSymbol);
        }

        public override IAsmSymbol[] GenerateParams(IAsmSymbol source)
        {
            if(source is IReadableAsmSymbol)
            {
                return new[] { source };
            }

            if(source is ITranslatableAsmSymbol tas)
            {
                return new[] { tas.Translate() };
            }

            throw new AssemblerException($"Non readable/translatable symbol passed as param operand.");
        }
    }
}

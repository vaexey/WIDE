using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;

namespace WIDEToolkit.Assembler.Assembly.Operand
{
    public class StringOperand : AsmInstructionOperand<LabelSymbol>
    {
        public string Text { get; set; }

        public StringOperand(string text) { Text = text; }

        public override bool Match(LabelSymbol symbol)
        {
            return base.Match(symbol) && symbol.Name == Text;
        }
    }
}

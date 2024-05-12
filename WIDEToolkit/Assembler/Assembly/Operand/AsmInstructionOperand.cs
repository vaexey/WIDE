using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Operand
{
    public abstract class AsmInstructionOperand
    {
        public abstract Type Expected { get; }

        public virtual IAsmSymbol[] GenerateParams(IAsmSymbol source) => Array.Empty<IAsmSymbol>();

        public virtual bool Match(IAsmSymbol symbol)
        {
            return symbol.GetType().IsAssignableTo(Expected);
        }
    }

    public abstract class AsmInstructionOperand<T> : AsmInstructionOperand where T : IAsmSymbol
    {
        public override Type Expected => typeof(T);

        public virtual bool Match(T symbol)
        {
            return true;
        }

        public override bool Match(IAsmSymbol symbol)
        {
            return 
                base.Match(symbol) &&
                Match((T) symbol);
        }
    }
}

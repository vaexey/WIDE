using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public interface ITranslatableAsmSymbol : IAsmSymbol
    {
        public void ProvideTable(Dictionary<string, WORD> labelTable);
        public IReadableAsmSymbol Translate();

        public int CalculateWidth(AsmImplementedInstruction impl);
    }
}

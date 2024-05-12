using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public class LabelSymbol : ITranslatableAsmSymbol
    {
        public string Name { get; set; }

        private Dictionary<string, WORD> labelTable = new();

        public LabelSymbol(string name)
        {
            Name = name;
        }

        public void ProvideTable(Dictionary<string, WORD> labelTable)
        {
            this.labelTable = labelTable;
        }

        public IReadableAsmSymbol Translate()
        {
            if (!labelTable.ContainsKey(Name))
                throw new AssemblerException($"Could not find \"{Name}\" label definition");

            var label = labelTable[Name];

            return new TranslatedLabelSymbol(
                Name,
                label.ToUInt64()
                );
        }

        public int CalculateWidth(AsmImplementedInstruction impl)
        {
            // TODO
            throw new NotImplementedException();
        }

        public string Reconstruct()
        {
            return Name;
        }
    }
}

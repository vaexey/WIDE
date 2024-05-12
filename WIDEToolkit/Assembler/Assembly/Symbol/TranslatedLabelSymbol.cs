using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    internal class TranslatedLabelSymbol : IReadableAsmSymbol
    {
        public string Name { get; set; }
        public ulong Value { get; set; }

        public TranslatedLabelSymbol(string name, ulong value = 0)
        {
            Name = name;
            Value = value;
        }

        public WORD Read() => WORD.FromUInt64(Value);

        public string Reconstruct()
        {
            return $"@0{Convert.ToString((long)Value, 16)}h";
        }
    }
}

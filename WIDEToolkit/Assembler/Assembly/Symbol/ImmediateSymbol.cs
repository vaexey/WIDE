using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public class ImmediateSymbol : IReadableAsmSymbol
    {
        public ulong Value { get; set; }

        public ImmediateSymbol(ulong value = 0)
        {
            Value = value;
        }

        public WORD Read() => WORD.FromUInt64(Value);

        public string Reconstruct()
        {
            return Value.ToString();
        }
    }
}

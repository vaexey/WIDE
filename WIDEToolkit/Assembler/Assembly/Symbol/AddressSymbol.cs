using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public class AddressSymbol : IAsmSymbol
    {
        public IAsmSymbol Address { get; set; }

        public AddressSymbol(IAsmSymbol address)
        {
            Address = address;
        }

        public string Reconstruct()
        {
            return $"[{Address.Reconstruct()}]";
        }
    }
}

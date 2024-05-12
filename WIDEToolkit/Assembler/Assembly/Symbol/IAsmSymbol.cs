using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public interface IAsmSymbol
    {
        public string Reconstruct();
    }
}

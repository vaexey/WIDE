using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public class NewLabelSymbol : IAsmSymbol
    {
        public string Name { get; set; }

        public NewLabelSymbol(string name)
        {
            Name = name;
        }

        public string Reconstruct()
        {
            return $"{Name}:";
        }
    }
}

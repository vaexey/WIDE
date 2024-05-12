using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Assembly.Symbol
{
    public class OffsetSymbol : IAsmSymbol
    {
        public int Operator { get; set; }

        public IAsmSymbol LHS { get; set; }
        public IAsmSymbol RHS { get; set; }

        public OffsetSymbol(IAsmSymbol lHS, IAsmSymbol rHS, int op = 1)
        {
            Operator = op;
            LHS = lHS;
            RHS = rHS;
        }

        public string Reconstruct()
        {
            return $"{LHS.Reconstruct()} {(Operator < 1 ? "-" : "+")} {RHS.Reconstruct()}";
        }
    }
}

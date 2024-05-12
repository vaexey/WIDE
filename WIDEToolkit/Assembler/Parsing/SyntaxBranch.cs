using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Assembler.Parsing
{
    public class SyntaxBranch
    {
        public string Origin { get; set; } = "";

        public string Operator { get; set; } = "";
        public string Before { get; set; } = "";

        public List<SyntaxBranch> Branches { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class ALUSignalBlueprint
    {
        public string NameFormat { get; set; } = "{0}";

        public List<string> SourceEndpointFormats { get; set; } = new();
        public List<string> DestEndpointFormats { get; set; } = new();

        public Regex NameRegex { get; set; } = new Regex("^.*$");

        public ALUOperation Operation { get; }

        public ALUSignalBlueprint( ALUOperation operation )
        {
            Operation = operation;
        }
    }
}

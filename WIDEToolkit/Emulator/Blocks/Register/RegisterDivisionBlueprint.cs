using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class RegisterDivisionBlueprint
    {
        public string NameFormat { get; set; } = "{0}";
        public List<RegisterSignalBlueprint> Signals { get; set; } = new();
        public Regex NameRegex { get; set; } = new Regex("^.*$");
        public int Start { get; set; }
        public int End { get; set; }

        public EndpointType EndpointType { get; set; } = EndpointType.BUS;

        public int Width { get => End - Start; }

        public RegisterDivisionBlueprint WithSignal(string nameF, string endpointF, RegisterSignalMode mode)
        {
            Signals.Add(new(nameF, endpointF, mode));

            return this;
        }

        public override string ToString()
        {
            return NameFormat;
        }
    }
}

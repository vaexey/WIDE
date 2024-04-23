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
        public string NameFormat = "{0}";
        public List<RegisterSignalBlueprint> Signals = new();
        public Regex NameRegex = new Regex("^.*$");
        public int Start;
        public int End;

        public EndpointType EndpointType = EndpointType.BUS;

        public int Width { get => End - Start; }

        public RegisterDivisionBlueprint WithSignal(string nameF, string endpointF, RegisterSignalMode mode)
        {
            Signals.Add(new(nameF, endpointF, mode));

            return this;
        }
    }
}

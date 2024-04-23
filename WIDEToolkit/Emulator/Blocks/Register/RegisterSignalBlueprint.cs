using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class RegisterSignalBlueprint
    {
        public string NameFormat { get; }
        public string EndpointFormat { get; }
        
        public RegisterSignalMode Mode { get; }

        public RegisterSignalBlueprint(string nameFormat, string endpointFormat, RegisterSignalMode mode)
        {
            NameFormat = nameFormat;
            EndpointFormat = endpointFormat;
            Mode = mode;
        }
    }
}

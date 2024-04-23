using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.MemHandler
{
    public class MemHandlerSignal : Signal
    {
        public string Endpoint { get; }
        public MemHandlerSignalType Type { get; }

        public MemHandlerSignal(string name, string endpoint, ArchBlock owner, MemHandlerSignalType type) : base(name, owner)
        {
            Endpoint = endpoint;
            Type = type;
        }
    }
}

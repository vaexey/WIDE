using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class RegisterSignal : Signal
    {

        public string Endpoint { get; }
        public int DivisionIndex { get; }
        public RegisterSignalMode Mode { get; }

        public RegisterSignal(string name, string endpoint, ArchBlock owner, int divIdx, RegisterSignalMode mode) : base(name, owner)
        {
            Endpoint = endpoint;
            DivisionIndex = divIdx;
            Mode = mode;
        }
    }
}

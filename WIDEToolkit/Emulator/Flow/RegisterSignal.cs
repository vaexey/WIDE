using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;

namespace WIDEToolkit.Emulator.Flow
{
    public class RegisterSignal : Signal
    {
        public enum RegisterSignalMode
        {
            NOOP = 0,
            LOAD = 1,
            STORE = 2,
            SUM = 3,
        }

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

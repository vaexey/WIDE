using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks
{
    public class InstructionRegister : Register
    {
        public enum InstrRegType
        {
            STATIC = 0,
            FLEXIBLE = 1
        }

        //public abstract InstrRegType Type { get; }
    }
}

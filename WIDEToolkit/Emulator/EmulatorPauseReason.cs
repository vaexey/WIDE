using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator
{
    public enum EmulatorPauseReason
    {
        UNKNOWN = 0,

        USER = 1,

        CYCLE = 2,
        INSTRUCTION = 4,

        EMULATOR_EXCEPTION = 256,
        DEBUG_EXCEPTION = 512
    }
}

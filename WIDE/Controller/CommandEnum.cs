using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.Controller
{
    public static class CommandEnum
    {
        public const string CPU_PAUSE = "cpu.emu.pause";
        public const string CPU_UNPAUSE = "cpu.emu.unpause";
        public const string CPU_STEP_CYCLE = "cpu.emu.cycle";
        public const string CPU_STEP_INSTRUCTION = "cpu.emu.instruction";
    }
}

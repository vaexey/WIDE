using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View;
using WIDE.View.CPU;

namespace WIDE.Controller
{
    public static class ScriptInitializer
    {
        public static void RegisterCommands(MainParentForm form)
        {
            var cmd = form.Commands;
            var emu = form.EContainer;

            cmd.Register(CommandEnum.CPU_PAUSE, () =>
            {
                if (emu.Emu.Paused)
                    throw new ScriptException(Texts.Script.ErrorEmulatorPaused);

                emu.InvokePause();
            });

            cmd.Register(CommandEnum.CPU_UNPAUSE, () =>
            {
                if (!emu.Emu.Paused)
                    throw new ScriptException(Texts.Script.ErrorEmulatorNotPaused);

                emu.InvokeUnpause();
            });

            cmd.Register(CommandEnum.CPU_STEP_CYCLE, () => emu.InvokeCycle());
            cmd.Register(CommandEnum.CPU_STEP_INSTRUCTION, () => emu.InvokeInstruction());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.Controller;

namespace WIDE.View.Toolbars
{
    public static class ToolbarItems
    {
        public static CommandToolStripButton Create(string command)
        {
            if (!creators.ContainsKey(command))
                throw new KeyNotFoundException($"Command {command} does not have an associated view button.");

            return creators[command]();
        }

        public static CommandToolStripButton Create(
            string? command,
            Image icon,
            string? description = null
        )
        {
            return new()
            {
                Command = command,
                Image = icon,
                ToolTipText = description ?? command
            };
        }

        private static KeyValuePair<string, Func<CommandToolStripButton>> MakePair(
            string command,
            Image icon,
            string? description = null
        )
        {
            return new(command, () =>
            {
                return Create(command, icon, description);
            });
        }

        private static Dictionary<string, Func<CommandToolStripButton>> creators = new(
            new[]
            {
                MakePair(CommandEnum.CPU_PAUSE, Resources.Pause, Texts.Script.EmulatorPause),
                MakePair(CommandEnum.CPU_UNPAUSE, Resources.Play, Texts.Script.EmulatorUnpause),
                MakePair(CommandEnum.CPU_STEP_CYCLE, Resources.t1_46, Texts.Script.EmulatorStepCycle),
                MakePair(CommandEnum.CPU_STEP_INSTRUCTION, Resources.Forward, Texts.Script.EmulatorStepInstruction)
            }
        );
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Toolbars
{
    public static class ToolbarItems
    {
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
                Text = description ?? command
            };
        }

        public static CommandToolStripButton ButtonArchLiveSet()
            => Create("cpu.live.set", Resources.Upload,
                "Set live");
        public static CommandToolStripButton ButtonArchLiveUnset()
            => Create("cpu.live.unset", Resources.t1_33,
                "Unset live");
    }
}

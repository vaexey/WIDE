using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Toolbars
{
    public class CommandToolStripButton : ToolStripButton
    {
        public string? Command { get; set; }

        public EventHandler<string> CommandTriggered { get; set; } = delegate { };

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if(Command is not null)
                CommandTriggered(this, Command);
        }
    }
}

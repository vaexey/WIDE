using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Toolbars
{
    internal class MemoryToolbar : ToolStrip
    {
        private ToolStripButton resetBtn;
        private ToolStripButton confirmBtn;

        public MemoryToolbar()
        {
            resetBtn = new()
            {
                Image = Resources.Erase
            };

            confirmBtn = new()
            {
                Image = Resources.t1_54
            };

            Items.Add(resetBtn);
            Items.Add(confirmBtn);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Toolbars
{
    internal class AsmToolbar : ToolStrip
    {
        private ToolStripButton assembleBtn;

        public AsmToolbar()
        {
            assembleBtn = new()
            {
                Image = Resources.t1_68
            };

            Items.Add(assembleBtn);
        }
    }
}

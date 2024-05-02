using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaTabControl;

namespace WIDE.View.Layout
{
    public class LayoutPickerEntry
    {

        public LayoutView Control { get; set; }
        public LayoutChild Target { get; set; }

        public LayoutChild? Owner { get;set; } = null;

        public string DisplayName => Control.Text;
        public bool IsUsedHere => Owner == Target;
        public bool IsUsedElsewhere => Owner is not null && Owner != Target;

        public LayoutPickerEntry(LayoutView c, LayoutChild target)
        {
            Control = c;
            Target = target;
        }
    }
}

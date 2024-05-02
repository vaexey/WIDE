using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View
{
    public class Translation
    {
        public class LayoutTranslation
        {
            public string Empty { get; } = "Empty";

            public string ButtonPick { get; } = "Select windows for this view.";
            public string ButtonCollapse { get; } = "(Un)collapse this view.";

            public string PickerFormTitle { get; } = "Select windows for this view...";
        }

        public class GlobalTranslation
        {
            public string OK { get; } = "OK";
            public string Cancel { get; } = "Cancel";
        }

        public LayoutTranslation Layout { get; } = new();
        public GlobalTranslation Global { get; } = new();
    }
}

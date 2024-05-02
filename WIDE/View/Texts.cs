using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WIDE.View.Translation;

namespace WIDE.View
{
    public static class Texts
    {
        public static Translation Translation { get; set;  } = new();

        public static LayoutTranslation Layout => Translation.Layout;
        public static GlobalTranslation Global => Translation.Global;
    }
}

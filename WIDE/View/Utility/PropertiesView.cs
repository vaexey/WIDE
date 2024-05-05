using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View.Layout;
using YaTabControl;

namespace WIDE.View.Utility
{
    public class PropertiesView : LayoutView
    {
        public PropertyGrid Grid;

        public PropertiesView()
        {
            Text = "Properties";
            Grid = new()
            {
                Dock = DockStyle.Fill,

                BackColor = Styles.ColorBackground,
                CategoryForeColor = Styles.ColorFont,

                ViewBackColor = Styles.ColorInner,
                ViewForeColor = Styles.ColorFont,

                CommandsBackColor = Styles.ColorBackground,
                CommandsForeColor = Styles.ColorFont,
                LineColor = Styles.ColorBackground,

                CategorySplitterColor = Styles.ColorFont,

                CommandsBorderColor = Styles.ColorFont,

                SelectedItemWithFocusBackColor = Styles.ColorSelection,
                SelectedItemWithFocusForeColor = Styles.ColorFont,

                HelpBackColor = Styles.ColorInner,
                HelpForeColor = Styles.ColorFont,

                //CommandsActiveLinkColor = Styles.ColorSelection,
                //CommandsDisabledLinkColor = Styles.ColorSelection,
                //CommandsLinkColor = Styles.ColorSelection,
                //DisabledItemForeColor = Styles.ColorSelection,
                //HelpBorderColor = Styles.ColorSelection,
                //ViewBorderColor = Styles.ColorSelection,
            };

            Controls.Add(Grid);
        }
    }
}

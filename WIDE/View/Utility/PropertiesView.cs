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
                Dock = DockStyle.Fill
            };

            Controls.Add(Grid);
        }
    }
}

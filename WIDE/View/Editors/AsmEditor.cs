using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View.Controls;
using WIDE.View.Layout;

namespace WIDE.View.Editors
{
    public class AsmEditor : LayoutView
    {
        Scintilla editor;
        
        public AsmEditor()
        {
            // TODO: translate
            Text = "Assembly";
            TabImage = Resources.Script;

            editor = new()
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(editor);

            AssemblerEditorInitializer.Init(editor);
        }
    }
}

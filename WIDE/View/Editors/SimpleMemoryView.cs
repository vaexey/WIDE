using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View.Layout;

namespace WIDE.View.Editors
{
    public class SimpleMemoryView : LayoutView
    {
        DataGridView memoryGrid;
        BindingSource bindingSource = new();

        public SimpleMemoryView()
        {
            // TODO: translate
            Text = "Memory (simple)";
            TabImage = Resources.Compass;

            memoryGrid = new()
            {
                Font = Styles.FontMonospace(Font.Size),
                BackgroundColor = Styles.ColorInner,
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,

                DataSource = bindingSource
            };

            memoryGrid.DefaultCellStyle.BackColor = Styles.ColorBackground;

            Controls.Add(memoryGrid);
        }

        protected override void OnMainFormReady(EventArgs e)
        {
            base.OnMainFormReady(e);

            // TODO: selectable memory
            var el = new EditableMemoryList(
                    MainForm.EContainer.Arch.MemoryBlock.Live.Divisions[0]
                );

            bindingSource.DataSource = el;

            //memoryGrid.DataSource = bs;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            
        }
    }
}

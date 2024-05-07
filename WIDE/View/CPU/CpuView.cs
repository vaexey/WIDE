using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WIDE.Controller;
using WIDE.View.Layout;
using WIDE.View.Utility;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Examples.W;
using YaTabControl;

namespace WIDE.View.CPU
{
    public class CpuView : LayoutView
    {
        PropertyGrid propertyGrid => MainForm.GetView<PropertiesView>().Grid;

        private System.Windows.Forms.Timer timer;
        private Label centerLabel;

        List<CpuElementControl> cpuElementControls = new();
        public ArchBlock? SelectedCpuBlock = null;

        public CpuView() : base()
        {
            Text = "CPU";
            TabImage = Resources.Computer_16x16;
            
            AutoScroll = true;

            timer = new()
            {
                Interval = 10,
                Enabled = true
            };
            timer.Tick += timer_Tick;

            centerLabel = new()
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = Texts.Emulator.CPUViewEmpty,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            Controls.Add(centerLabel);

            HandleCreated += cpuView_Handle;
            Click += cpuView_Click;
        }

        protected override void OnMainFormReady(EventArgs e)
        {
            base.OnMainFormReady(e);

            propertyGrid.PropertyValueChanged += cpuProperties_PropertyValueChanged;
        }

        private void cpuView_Handle(object? sender, EventArgs e)
        {
            DrawArch();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            foreach (var cpb in cpuElementControls)
                cpb.UpdateText();
        }

        private void cpuView_Click(object? sender, EventArgs e)
        {
            SelectBlock(null);
        }

        private void cpuView_element_Click(object? sender, EventArgs e)
        {
            if (sender is CpuElementControl cec)
            {
                SelectBlock(cec.Block);
            }
        }

        private void cpuView_meta_changed(object? sender, EventArgs e)
        {
            if (sender is CpuElementControl cec)
            {
                if (propertyGrid.SelectedObject == cec.Block)
                    propertyGrid.Refresh();
            }
        }

        private void cpuProperties_PropertyValueChanged(object? s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem is GridItem gi
                && gi.GetType().GetProperty("Instance") is PropertyInfo prop
                && prop.GetValue(gi) is BlockMetadata bm)
            {
                foreach (var cec in cpuElementControls.Where(x => x.Block == SelectedCpuBlock))
                {
                    cec.UpdatePosition();
                }
            }
        }

        private void SelectBlock(ArchBlock? b)
        {
            if (SelectedCpuBlock == b) return;

            SelectedCpuBlock = b;

            if (b is null)
            {
                //propertyGrid.SelectedObject = null;
                if(MainForm.EContainer is not null)
                    propertyGrid.SelectedObject = MainForm.EContainer.Arch;

                return;
            }

            propertyGrid.SelectedObject = b;
        }

        public void DrawArch()
        {
            foreach (var cpb in cpuElementControls)
                Controls.Remove(cpb);

            cpuElementControls.Clear();
            
            if(!MainForm.EContainer.Arch.Blocks.Where(b => !b.Meta.Hidden).Any())
            {
                centerLabel.Visible = true;

                return;
            }

            centerLabel.Visible = false;

            foreach (var b in MainForm.EContainer.Arch.Blocks)
            {
                var meta = b.Meta;

                if (meta.Hidden)
                    continue;

                var cpb = new CpuElementControl(b);

                cpb.UpdatePosition();

                Controls.Add(cpb);
                cpuElementControls.Add(cpb);

                cpb.Click += cpuView_element_Click;
                cpb.MetaPositionChanged += cpuView_meta_changed;
            }
        }

        public void Command_ResetEmulator()
        {
            
        }
    }
}

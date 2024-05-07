using Be.Windows.Forms;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIDE.Controller;
using WIDE.View;
using WIDE.View.CPU;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Examples.W;

namespace WIDE
{
    public partial class ProjectForm : Form
    {
        Scintilla assemblerEditor;
        HexBox memoryEditor;

        EmulatorContainer emu;

        List<CpuElementControl> cpuElementControls = new();
        public ArchBlock? SelectedCpuBlock = null;

        public ProjectForm(EmulatorContainer econt)
        {
            InitializeComponent();

            tabControl.ImageList = new ImageList();

            tabControl.ImageList.Images.Add("cpu", Resources.Computer_16x16);
            tabPageCPU.ImageKey = "cpu";

            tabControl.ImageList.Images.Add("memory", Resources.Database);
            tabPageMemory.ImageKey = "memory";

            tabControl.ImageList.Images.Add("asm", Resources.Script);
            tabPageAsm.ImageKey = "asm";

            assemblerEditor = new();
            assemblerPanel.Controls.Add(assemblerEditor);

            //AssemblerEditorInitializer.Init(assemblerEditor);

            memoryEditor = new();
            memoryPanel.Controls.Add(memoryEditor);

            //HexEditorInitializer.Init(memoryEditor);

            //emu = econt;

            //var a = new WArchitecture();
            //a.CreateLive();
            //Emulator e = new(a, new WRawInstructionSet(a));

            //emu = new(a) { Emu = e };
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            DrawArch();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            foreach (var cpb in cpuElementControls)
                cpb.UpdateText();
        }

        private void cpuPanel_Click(object sender, EventArgs e)
        {
            SelectBlock(null);
        }

        private void cpuPanel_element_Click(object? sender, EventArgs e)
        {
            if (sender is CpuElementControl cec)
            {
                SelectBlock(cec.Block);
            }
        }

        private void cpuPanel_meta_changed(object? sender, EventArgs e)
        {
            if (sender is CpuElementControl cec)
            {
                if (cpuProperties.SelectedObject == cec.Block)
                    cpuProperties.Refresh();
            }
        }

        private void cpuProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
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
                cpuProperties.SelectedObject = null;

                return;
            }

            cpuProperties.SelectedObject = b;
        }

        private void DrawArch()
        {
            cpuPanel.Controls.Clear();
            cpuElementControls.Clear();

            foreach (var b in emu.Emu.Arch.Blocks)
            {
                var meta = b.Meta;

                if (meta.Hidden)
                    continue;

                var cpb = new CpuElementControl(b);

                cpb.UpdatePosition();

                cpuPanel.Controls.Add(cpb);
                cpuElementControls.Add(cpb);

                cpb.Click += cpuPanel_element_Click;
                cpb.MetaPositionChanged += cpuPanel_meta_changed;
            }
        }
    }
}

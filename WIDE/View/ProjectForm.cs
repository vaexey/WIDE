using Be.Windows.Forms;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIDE.Controller;
using WIDE.Examples.W;
using WIDE.View;
using WIDE.View.CPU;
using WIDEToolkit.Emulator;
using WIDEToolkit.Examples.W;

namespace WIDE
{
    public partial class ProjectForm : Form
    {
        Scintilla assemblerEditor;
        HexBox memoryEditor;

        EmulatorContainer emu;

        public ProjectForm()
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

            AssemblerEditorInitializer.Init(assemblerEditor);

            memoryEditor = new();
            memoryPanel.Controls.Add(memoryEditor);

            HexEditorInitializer.Init(memoryEditor);

            var a = new WArchitecture();
            Emulator e = new(a, new WRawInstructionSet(a));

            emu = new(e);
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            DrawArch();
        }

        private void DrawArch()
        {
            cpuPanel.Controls.Clear();

            foreach(var b in emu.Emu.Arch.Blocks)
            {
                var meta = b.Meta;

                var cpb = new CpuElementControl();

                cpuPanel.Controls.Add(cpb);

                cpb.Text = meta.Title;
                cpb.Left = meta.X;
                cpb.Top = meta.Y;

                cpb.Size = new(100, 100);
            }
        }
    }
}

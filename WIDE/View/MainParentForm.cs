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
using WIDE.View.Controls;
using WIDE.View.CPU;
using WIDE.View.Editors;
using WIDE.View.Toolbars;
using WIDE.View.Utility;
using WIDEToolkit.Emulator;
using WIDEToolkit.Examples.W;
using YaTabControl;

namespace WIDE.View
{
    public partial class MainParentForm : Form
    {
        public EmulatorContainer EContainer = new();

        public ScriptEngine Commands = new();

        CpuView CpuView = new();
        RawMemoryView RawMemoryView = new();
        PropertiesView PropertiesView = new();
        StatusView StatusView = new();

        public EventHandler LayoutReady = delegate { };

        private IEnumerable<YaTabPage> Views => new YaTabPage[] {
            CpuView, PropertiesView
        };

        public T GetView<T>() where T : YaTabPage
        {
            var acq = Views.OfType<T>().FirstOrDefault();

            return acq ?? throw new NullReferenceException($"{typeof(T)} does not exist on MainParentForm");
        }

        public MainParentForm()
        {
            InitializeComponent();

            ForeColor = Styles.ColorFont;
            BackColor = Styles.ColorBackground;
            Font = Styles.FontSans(Font.Size);
            Text = Texts.Global.WindowTitle;
            Icon = Resources.WIDE_icon;

            titleMenuStrip.BackColor = Styles.ColorBackground;
            titleMenuStrip.ForeColor = Styles.ColorFont;
            titleMenuStrip.Renderer = new StyledToolStripRenderer();

            defaultToolStrip.Items.AddRange(new ToolStripItem[]
            {
                ToolbarItems.Create(CommandEnum.CPU_UNPAUSE),
                ToolbarItems.Create(CommandEnum.CPU_PAUSE),
                ToolbarItems.Create(CommandEnum.CPU_STEP_CYCLE),
                ToolbarItems.Create(CommandEnum.CPU_STEP_INSTRUCTION),
            });

            foreach (var item in defaultToolStrip.Items)
            {
                if(item is CommandToolStripButton ctsb)
                    ctsb.CommandTriggered += delegate(object? s, string st)
                    {
                        Commands.Execute(st);
                        };
            }

            ScriptInitializer.RegisterCommands(this);
        }

        private void MainParentForm_Load(object sender, EventArgs e)
        {
            layoutContainer.ViewControls.Add(CpuView);
            layoutContainer.ViewControls.Add(RawMemoryView);
            layoutContainer.ViewControls.Add(PropertiesView);
            layoutContainer.ViewControls.Add(StatusView);

            layoutContainer.SetViewOwner(CpuView, layoutContainer.ChildCenter);
            layoutContainer.SetViewOwner(RawMemoryView, layoutContainer.ChildLeft);
            layoutContainer.SetViewOwner(PropertiesView, layoutContainer.ChildRight);
            layoutContainer.SetViewOwner(StatusView, layoutContainer.ChildBottom);

            LayoutReady(this, EventArgs.Empty);

            CpuView.DrawArch();

            EContainer.Start();
        }

        private void cpuStripMenuItem_Click(object sender, EventArgs e)
        {
            layoutContainer.SetViewOwner(CpuView, layoutContainer.ChildCenter);
        }
        
    }
}

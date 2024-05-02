using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIDE.View.Controls;
using WIDE.View.CPU;
using WIDE.View.Utility;
using YaTabControl;

namespace WIDE.View
{
    public partial class MainParentForm : Form
    {
        CpuView Cpu = new();

        PropertiesView Properties = new();

        public EventHandler LayoutReady = delegate { };

        private IEnumerable<YaTabPage> Views => new YaTabPage[] {
            Cpu, Properties
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

            titleMenuStrip.BackColor = Styles.ColorBackground;
            titleMenuStrip.ForeColor = Styles.ColorFont;
            titleMenuStrip.Renderer = new StyledToolStripRenderer();
        }

        private void MainParentForm_Load(object sender, EventArgs e)
        {
            layoutContainer.ViewControls.Add(Cpu);
            layoutContainer.ViewControls.Add(Properties);

            layoutContainer.SetViewOwner(Cpu, layoutContainer.ChildCenter);
            layoutContainer.SetViewOwner(Properties, layoutContainer.ChildRight);

            LayoutReady(this, EventArgs.Empty);
        }
    }
}

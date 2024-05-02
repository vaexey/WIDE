using WIDE.Controller;
using WIDE.Examples.W;
using WIDE.View;
using WIDEToolkit.Emulator;

namespace WIDE
{
    public partial class StartForm : Form
    {
        ProjectForm? projectForm;
        MainParentForm? parentForm;

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            WArchitecture warch = new();

            EmulatorContainer econt = new(warch);

            projectForm = new(econt);
            //projectForm.Show();

            parentForm = new();
            parentForm.Show();
        }
    }
}
using WIDE.Controller;
using WIDE.View;
using WIDEToolkit.Emulator;

namespace WIDE
{
    public partial class StartForm : Form
    {
        //ProjectForm? projectForm;
        MainParentForm? parentForm;

        DateTime start = DateTime.Now;
        int splashSeconds = 3;

        public StartForm()
        {
            InitializeComponent();

#if DEBUG
            splashSeconds = 0;
#endif
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            start = DateTime.Now;
            //WArchitecture warch = new();

            //EmulatorContainer econt = new(warch);

            //projectForm = new(econt);
            //projectForm.Show

            parentForm = new();

            parentForm.FormClosed += MainForm_Closed;

            parentForm.ShowInTaskbar = false;
            parentForm.WindowState = FormWindowState.Minimized;
            parentForm.Show();
        }

        private void startTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(start).TotalSeconds > splashSeconds)
            {
                ShowMainForm();
            }
        }

        private void ShowMainForm()
        {
            Hide();

            //if (parentForm is null || parentForm.Visible)
            //    return;

            startTimer.Stop();

            parentForm.ShowInTaskbar = true;
            parentForm.WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Closed(object? sender, EventArgs e)
        {
            Close();
        }

        private void StartForm_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }
    }
}
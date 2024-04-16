namespace WIDE
{
    public partial class StartForm : Form
    {
        ProjectForm projectForm = new();

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            projectForm.Show();
        }
    }
}
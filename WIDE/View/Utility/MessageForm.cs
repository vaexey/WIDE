using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIDE.View.Utility
{
    public partial class MessageForm : Form
    {
        public MessageForm(string title, string text, bool error = false)
        {
            InitializeComponent();

            Text = Texts.Global.WindowTitle;
            titleLabel.Text = title;
            messageLabel.Text = text;

            //TODO border color
            ForeColor = Styles.ColorFont;
            BackColor = Styles.ColorBackground;
            messageLabel.BackColor = Styles.ColorInner;

            okButton.ForeColor = SystemColors.ControlText;
            okButton.Text = Texts.Global.OK;

            iconBox.Image = error ? Resources.Error : Resources.Warning;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}

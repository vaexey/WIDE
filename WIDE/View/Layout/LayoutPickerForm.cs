using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIDE.View.Layout
{
    public partial class LayoutPickerForm : Form
    {
        public List<LayoutPickerEntry> Entries { get; set; } = new();

        public List<LayoutPickerEntry> Modified { get; set; } = new();

        public LayoutPickerForm()
        {
            InitializeComponent();

            Text = Texts.Layout.PickerFormTitle;

            okButton.Text = Texts.Global.OK;
            cancelButton.Text = Texts.Global.Cancel;

            BackColor = Styles.ColorBackground;
            //ForeColor = Styles.ColorFont;
        }

        private void LayoutPickerForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                var ent = Entries[i];

                pickerBox.Items.Add(ent.DisplayName);

                pickerBox.SetItemCheckState(i,
                    ent.IsUsedElsewhere ? CheckState.Indeterminate :
                    (ent.IsUsedHere ? CheckState.Checked : CheckState.Unchecked));
            }

            Modified.Clear();
        }

        private void pickerBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.CurrentValue == CheckState.Indeterminate)
            //    e.NewValue = CheckState.Indeterminate;
            //else
            //{
            if (e.CurrentValue == CheckState.Indeterminate)
                e.NewValue = CheckState.Checked;

            var ent = Entries[e.Index];

            ent.Owner = (e.NewValue == CheckState.Checked) ? ent.Target : null;

            Modified.Add(ent);
            //}
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

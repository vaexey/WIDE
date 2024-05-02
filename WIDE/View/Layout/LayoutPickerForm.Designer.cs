namespace WIDE.View.Layout
{
    partial class LayoutPickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pickerBox = new CheckedListBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // pickerBox
            // 
            pickerBox.FormattingEnabled = true;
            pickerBox.Location = new Point(12, 12);
            pickerBox.Name = "pickerBox";
            pickerBox.Size = new Size(410, 202);
            pickerBox.TabIndex = 0;
            pickerBox.ItemCheck += pickerBox_ItemCheck;
            // 
            // okButton
            // 
            okButton.Location = new Point(131, 226);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "button1";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(212, 226);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "button2";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // LayoutPickerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 261);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(pickerBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "LayoutPickerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "LayoutPickerForm";
            Load += LayoutPickerForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox pickerBox;
        private Button okButton;
        private Button cancelButton;
    }
}
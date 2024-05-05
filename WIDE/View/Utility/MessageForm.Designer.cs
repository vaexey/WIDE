namespace WIDE.View.Utility
{
    partial class MessageForm
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
            okButton = new Button();
            messageLabel = new Label();
            titleLabel = new Label();
            iconBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)iconBox).BeginInit();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Location = new Point(131, 226);
            okButton.Name = "okButton";
            okButton.Size = new Size(156, 23);
            okButton.TabIndex = 3;
            okButton.Text = "button1";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // messageLabel
            // 
            messageLabel.BorderStyle = BorderStyle.FixedSingle;
            messageLabel.Location = new Point(14, 52);
            messageLabel.Margin = new Padding(5);
            messageLabel.Name = "messageLabel";
            messageLabel.Padding = new Padding(5);
            messageLabel.Size = new Size(406, 166);
            messageLabel.TabIndex = 5;
            messageLabel.Text = "label1";
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 16F);
            titleLabel.Location = new Point(50, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(370, 32);
            titleLabel.TabIndex = 6;
            titleLabel.Text = "label1";
            // 
            // iconBox
            // 
            iconBox.Location = new Point(12, 12);
            iconBox.Name = "iconBox";
            iconBox.Size = new Size(32, 32);
            iconBox.SizeMode = PictureBoxSizeMode.Zoom;
            iconBox.TabIndex = 7;
            iconBox.TabStop = false;
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 261);
            Controls.Add(iconBox);
            Controls.Add(titleLabel);
            Controls.Add(messageLabel);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "MessageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MessageForm";
            ((System.ComponentModel.ISupportInitialize)iconBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button okButton;
        private Label messageLabel;
        private Label titleLabel;
        private PictureBox iconBox;
    }
}
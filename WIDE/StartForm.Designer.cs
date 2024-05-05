namespace WIDE
{
    partial class StartForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            startTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // startTimer
            // 
            startTimer.Enabled = true;
            startTimer.Tick += startTimer_Tick;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Resources.WIDE_splash;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(640, 320);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "StartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            TopMost = true;
            Load += StartForm_Load;
            Click += StartForm_Click;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer startTimer;
    }
}
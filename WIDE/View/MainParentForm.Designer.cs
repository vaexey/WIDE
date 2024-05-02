namespace WIDE.View
{
    partial class MainParentForm
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
            components = new System.ComponentModel.Container();
            layoutContainer = new Layout.LayoutContainer();
            ToolTipControl = new ToolTip(components);
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            titleMenuStrip = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            titleMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // layoutContainer
            // 
            layoutContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            layoutContainer.Location = new Point(0, 74);
            layoutContainer.Margin = new Padding(0);
            layoutContainer.Name = "layoutContainer";
            layoutContainer.Size = new Size(1050, 413);
            layoutContainer.TabIndex = 0;
            layoutContainer.Text = "layoutContainer1";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Resources.WIDE_logo;
            pictureBox1.Location = new Point(976, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(74, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.Location = new Point(547, 29);
            label3.Name = "label3";
            label3.Size = new Size(331, 20);
            label3.TabIndex = 7;
            label3.Text = "W-machine Integrated Development Environment";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Courier New", 11.25F, FontStyle.Bold);
            label2.Location = new Point(895, 49);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 6;
            label2.Text = "v. 0.1.0";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Courier New", 27.75F, FontStyle.Bold);
            label1.Location = new Point(877, 16);
            label1.Name = "label1";
            label1.Size = new Size(106, 41);
            label1.TabIndex = 5;
            label1.Text = "WIDE";
            // 
            // titleMenuStrip
            // 
            titleMenuStrip.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleMenuStrip.AutoSize = false;
            titleMenuStrip.Dock = DockStyle.None;
            titleMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            titleMenuStrip.Location = new Point(0, 0);
            titleMenuStrip.Name = "titleMenuStrip";
            titleMenuStrip.Size = new Size(975, 24);
            titleMenuStrip.TabIndex = 9;
            titleMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(125, 20);
            toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(180, 22);
            toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // MainParentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 487);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(layoutContainer);
            Controls.Add(titleMenuStrip);
            MainMenuStrip = titleMenuStrip;
            MinimumSize = new Size(640, 480);
            Name = "MainParentForm";
            Text = "MDIParentForm";
            Load += MainParentForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            titleMenuStrip.ResumeLayout(false);
            titleMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Layout.LayoutContainer layoutContainer;
        public ToolTip ToolTipControl;
        private PictureBox pictureBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private MenuStrip titleMenuStrip;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
    }
}
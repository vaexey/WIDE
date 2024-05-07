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
                EContainer.Dispose();
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
            titleMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            viewMenuStripItem = new ToolStripMenuItem();
            cpuStripMenuItem = new ToolStripMenuItem();
            defaultToolStrip = new ToolStrip();
            logoControl = new Controls.LogoControl();
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
            // titleMenuStrip
            // 
            titleMenuStrip.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleMenuStrip.AutoSize = false;
            titleMenuStrip.Dock = DockStyle.None;
            titleMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewMenuStripItem });
            titleMenuStrip.Location = new Point(0, 0);
            titleMenuStrip.Name = "titleMenuStrip";
            titleMenuStrip.Size = new Size(975, 24);
            titleMenuStrip.TabIndex = 9;
            titleMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // viewMenuStripItem
            // 
            viewMenuStripItem.DropDownItems.AddRange(new ToolStripItem[] { cpuStripMenuItem });
            viewMenuStripItem.Name = "viewMenuStripItem";
            viewMenuStripItem.Size = new Size(44, 20);
            viewMenuStripItem.Text = "View";
            // 
            // cpuStripMenuItem
            // 
            cpuStripMenuItem.Name = "cpuStripMenuItem";
            cpuStripMenuItem.Size = new Size(97, 22);
            cpuStripMenuItem.Text = "CPU";
            cpuStripMenuItem.Click += cpuStripMenuItem_Click;
            // 
            // defaultToolStrip
            // 
            defaultToolStrip.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            defaultToolStrip.AutoSize = false;
            defaultToolStrip.BackColor = Color.FromArgb(64, 64, 64);
            defaultToolStrip.Dock = DockStyle.None;
            defaultToolStrip.Location = new Point(9, 41);
            defaultToolStrip.Name = "defaultToolStrip";
            defaultToolStrip.Size = new Size(866, 25);
            defaultToolStrip.TabIndex = 10;
            defaultToolStrip.Text = "toolStrip1";
            // 
            // logoControl
            // 
            logoControl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            logoControl.Location = new Point(556, 0);
            logoControl.Name = "logoControl";
            logoControl.Size = new Size(494, 74);
            logoControl.TabIndex = 11;
            // 
            // MainParentForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1050, 487);
            Controls.Add(defaultToolStrip);
            Controls.Add(layoutContainer);
            Controls.Add(titleMenuStrip);
            Controls.Add(logoControl);
            DoubleBuffered = true;
            MainMenuStrip = titleMenuStrip;
            MinimumSize = new Size(640, 480);
            Name = "MainParentForm";
            Text = "MainParentForm";
            Load += MainParentForm_Load;
            titleMenuStrip.ResumeLayout(false);
            titleMenuStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Layout.LayoutContainer layoutContainer;
        public ToolTip ToolTipControl;
        private MenuStrip titleMenuStrip;
        private ToolStripMenuItem viewMenuStripItem;
        private ToolStripMenuItem cpuStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStrip defaultToolStrip;
        private Controls.LogoControl logoControl;
    }
}
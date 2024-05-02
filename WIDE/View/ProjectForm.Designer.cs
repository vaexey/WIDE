namespace WIDE
{
    partial class ProjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
            tabControl = new TabControl();
            tabPageCPU = new TabPage();
            cpuToolbar1 = new View.Toolbars.CpuToolbar();
            cpuPanel = new Panel();
            cpuProperties = new PropertyGrid();
            tabPageMemory = new TabPage();
            memoryToolbar1 = new View.Toolbars.MemoryToolbar();
            memoryPanel = new Panel();
            tabPageAsm = new TabPage();
            asmToolbar1 = new View.Toolbars.AsmToolbar();
            assemblerPanel = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            mainTimer = new System.Windows.Forms.Timer(components);
            tabControl.SuspendLayout();
            tabPageCPU.SuspendLayout();
            tabPageMemory.SuspendLayout();
            tabPageAsm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabPageCPU);
            tabControl.Controls.Add(tabPageMemory);
            tabControl.Controls.Add(tabPageAsm);
            tabControl.Location = new Point(0, 53);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(957, 532);
            tabControl.TabIndex = 0;
            // 
            // tabPageCPU
            // 
            tabPageCPU.Controls.Add(cpuToolbar1);
            tabPageCPU.Controls.Add(cpuPanel);
            tabPageCPU.Controls.Add(cpuProperties);
            tabPageCPU.Location = new Point(4, 24);
            tabPageCPU.Margin = new Padding(0);
            tabPageCPU.Name = "tabPageCPU";
            tabPageCPU.Size = new Size(949, 504);
            tabPageCPU.TabIndex = 0;
            tabPageCPU.Text = "CPU";
            tabPageCPU.UseVisualStyleBackColor = true;
            // 
            // cpuToolbar1
            // 
            cpuToolbar1.Location = new Point(0, 0);
            cpuToolbar1.Name = "cpuToolbar1";
            cpuToolbar1.Size = new Size(709, 25);
            cpuToolbar1.TabIndex = 2;
            cpuToolbar1.Text = "cpuToolbar1";
            // 
            // cpuPanel
            // 
            cpuPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cpuPanel.Location = new Point(0, 28);
            cpuPanel.Name = "cpuPanel";
            cpuPanel.Size = new Size(706, 476);
            cpuPanel.TabIndex = 1;
            cpuPanel.Click += cpuPanel_Click;
            // 
            // cpuProperties
            // 
            cpuProperties.Dock = DockStyle.Right;
            cpuProperties.Location = new Point(709, 0);
            cpuProperties.Name = "cpuProperties";
            cpuProperties.Size = new Size(240, 504);
            cpuProperties.TabIndex = 0;
            cpuProperties.PropertyValueChanged += cpuProperties_PropertyValueChanged;
            // 
            // tabPageMemory
            // 
            tabPageMemory.Controls.Add(memoryToolbar1);
            tabPageMemory.Controls.Add(memoryPanel);
            tabPageMemory.Location = new Point(4, 24);
            tabPageMemory.Name = "tabPageMemory";
            tabPageMemory.Size = new Size(949, 504);
            tabPageMemory.TabIndex = 1;
            tabPageMemory.Text = "Memory";
            tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // memoryToolbar1
            // 
            memoryToolbar1.Location = new Point(0, 0);
            memoryToolbar1.Name = "memoryToolbar1";
            memoryToolbar1.Size = new Size(949, 25);
            memoryToolbar1.TabIndex = 1;
            memoryToolbar1.Text = "memoryToolbar1";
            // 
            // memoryPanel
            // 
            memoryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            memoryPanel.Location = new Point(0, 28);
            memoryPanel.Name = "memoryPanel";
            memoryPanel.Size = new Size(949, 476);
            memoryPanel.TabIndex = 0;
            // 
            // tabPageAsm
            // 
            tabPageAsm.Controls.Add(asmToolbar1);
            tabPageAsm.Controls.Add(assemblerPanel);
            tabPageAsm.Location = new Point(4, 24);
            tabPageAsm.Name = "tabPageAsm";
            tabPageAsm.Size = new Size(949, 504);
            tabPageAsm.TabIndex = 2;
            tabPageAsm.Text = "Assembler";
            tabPageAsm.UseVisualStyleBackColor = true;
            // 
            // asmToolbar1
            // 
            asmToolbar1.Location = new Point(0, 0);
            asmToolbar1.Name = "asmToolbar1";
            asmToolbar1.Size = new Size(949, 25);
            asmToolbar1.TabIndex = 1;
            asmToolbar1.Text = "asmToolbar1";
            // 
            // assemblerPanel
            // 
            assemblerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            assemblerPanel.Location = new Point(0, 28);
            assemblerPanel.Name = "assemblerPanel";
            assemblerPanel.Size = new Size(949, 476);
            assemblerPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Courier New", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(784, 16);
            label1.Name = "label1";
            label1.Size = new Size(106, 41);
            label1.TabIndex = 1;
            label1.Text = "WIDE";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Courier New", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(802, 49);
            label2.Name = "label2";
            label2.Size = new Size(80, 17);
            label2.TabIndex = 2;
            label2.Text = "v. 0.1.0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(509, 29);
            label3.Name = "label3";
            label3.Size = new Size(271, 15);
            label3.TabIndex = 3;
            label3.Text = "W-machine Integrated Development Environment";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Resources.WIDE_logo;
            pictureBox1.Location = new Point(883, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(74, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // mainTimer
            // 
            mainTimer.Enabled = true;
            mainTimer.Tick += mainTimer_Tick;
            // 
            // ProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(957, 585);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProjectForm";
            Text = "WIDE";
            Load += ProjectForm_Load;
            tabControl.ResumeLayout(false);
            tabPageCPU.ResumeLayout(false);
            tabPageCPU.PerformLayout();
            tabPageMemory.ResumeLayout(false);
            tabPageMemory.PerformLayout();
            tabPageAsm.ResumeLayout(false);
            tabPageAsm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPageCPU;
        private TabPage tabPageMemory;
        private TabPage tabPageAsm;
        private Panel assemblerPanel;
        private PropertyGrid cpuProperties;
        private Panel memoryPanel;
        private Label label1;
        private Panel cpuPanel;
        private Label label2;
        private Label label3;
        private View.Toolbars.MemoryToolbar memoryToolbar1;
        private View.Toolbars.AsmToolbar asmToolbar1;
        private View.Toolbars.CpuToolbar cpuToolbar1;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer mainTimer;
    }
}
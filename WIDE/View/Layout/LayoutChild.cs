using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View.Controls;
using YaTabControl;
using ytc = YaTabControl;

namespace WIDE.View.Layout
{
    public class LayoutChild : Panel
    {
        public Panel TitlePanel { get; set; }

        protected RotLabel TitleLabel { get; set; }
        protected Button CollapseButton { get; set; }
        protected Button PickButton { get; set; }

        public ytc.YaTabControl Content { get; set; }
        protected ytc.YaTabDrawerBase ContentTabDrawer { get; set; }

        public int CollapsedSize { get; set; } = 20;
        protected int LastUncollapsedSize { get; set; } = 250;

        public EventHandler PickerButtonClick { get; set; } = delegate { };

        public bool Collapsed {
            get
            {
                switch(Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        return Width == CollapsedSize;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        return Height == CollapsedSize;
                    default:
                        throw new InvalidOperationException("Cannot access Collapsed property on a non-docked LayoutChild.");
                }
            }
            set
            {
                switch (Dock)
                {
                    case DockStyle.Left:
                    case DockStyle.Right:
                        if (Width == CollapsedSize)
                        {
                            Width = LastUncollapsedSize;
                            return;
                        }

                        LastUncollapsedSize = Width;
                        Width = CollapsedSize;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        if(Height == CollapsedSize)
                        {
                            Height = LastUncollapsedSize;
                            return;
                        }

                        LastUncollapsedSize = Height;
                        Height = CollapsedSize;
                        break;
                    default:
                        throw new InvalidOperationException("Cannot access Collapsed property on a non-docked LayoutChild.");
                }
            }
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                TitleLabel.Text = value;
                base.Text = value;
            }
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);

            TitlePanel.Dock = Dock;

            //Content.TabDock = Dock;

            switch (Dock)
            { 
                case DockStyle.Left:
                case DockStyle.Right:
                    TitlePanel.Visible = true;
                    CollapseButton.Location = new Point(2, 2);
                    PickButton.Location = new Point(2, 18);
                    TitleLabel.Location = new Point(2, 36);
                    TitleLabel.Size = new Size(18, 1000);
                    TitleLabel.RotationAngle = 90;

                    break;
                case DockStyle.Top:
                case DockStyle.Bottom:
                    TitlePanel.Visible = true;
                    CollapseButton.Location = new Point(2, 2);
                    PickButton.Location = new Point(18, 2);
                    TitleLabel.Location = new Point(36, 2);
                    TitleLabel.Size = new Size(1000, 18);
                    TitleLabel.RotationAngle = 0;

                    break;
                default:
                    TitlePanel.Visible = false;
                    break;
            }

            if (Dock != DockStyle.None && Content.Controls.Count == 0)
            {
                Collapsed = true;
                CollapseButton.Enabled = false;
            }
            
            Content_ControlsCountChanged(null, e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            CollapseButton.SetToolTip(Texts.Layout.ButtonCollapse);
            PickButton.SetToolTip(Texts.Layout.ButtonPick);
        }

        public LayoutChild()
        {
            Size = new Size(250, 250);
            BorderStyle = BorderStyle.FixedSingle;

            TitlePanel = new()
            {
                BackColor = Styles.ColorCollapseBar,
                Size = new(CollapsedSize, CollapsedSize),
                Visible = false
            };
            Controls.Add(TitlePanel);

            TitleLabel = new()
            {

            };
            TitlePanel.Controls.Add(TitleLabel);

            CollapseButton = new()
            {
                Width = 16,
                Height = 16,
                Image = Resources.Bottom,
                FlatStyle = FlatStyle.Flat,
                
            };
            CollapseButton.Click += CollapseButton_Click;
            TitlePanel.Controls.Add(CollapseButton);

            PickButton = new()
            {
                Width = 16,
                Height = 16,
                Image = Resources.Blue_pin,
                FlatStyle = FlatStyle.Flat
            };
            PickButton.Click += PickButton_Click;
            TitlePanel.Controls.Add(PickButton);

            ContentTabDrawer = new VsTabDrawer();

            Content = new()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ScrollButtonStyle = ytc.YaScrollButtonStyle.Never,

                ForeColor = Styles.ColorFont,
                BackColor = Styles.ColorInner,

                ActiveColor = Styles.ColorBackground,

                InactiveColor = Styles.ColorInner,
                TabDrawer = ContentTabDrawer

            };
            Content.MouseMove += Content_MouseMove;
            Content.MouseUp += Content_MouseUp;
            Content.MouseDown += Content_MouseDown;
            Controls.Add(Content);

            Content.ControlAdded += Content_ControlsCountChanged;
            Content.ControlRemoved += Content_ControlsCountChanged;

            Text = Texts.Layout.Empty;
        }

        protected void Content_MouseMove(object? sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        protected void Content_MouseDown(object? sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected void Content_MouseUp(object? sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            switch (Dock)
            {
                case DockStyle.Left:
                    Content.Location = new(TitlePanel.Width, 0);
                    Content.Size = new(Width - TitlePanel.Width, Height);
                    break;
                case DockStyle.Right:
                    Content.Location = new(0, 0);
                    Content.Size = new(Width - TitlePanel.Width, Height);
                    break;
                case DockStyle.Top:
                    Content.Location = new(0, TitlePanel.Height);
                    Content.Size = new(Width, Height - TitlePanel.Height);
                    break;
                case DockStyle.Bottom:
                    Content.Location = new(0, 0);
                    Content.Size = new(Width, Height - TitlePanel.Height);
                    break;
            }
        }

        protected void CollapseButton_Click(object? sender, EventArgs e)
        {
            Collapsed = !Collapsed;
        }

        protected void PickButton_Click(object? sender, EventArgs e)
        {
            PickerButtonClick(this, e);
        }

        protected void Content_ControlsCountChanged(object? sender, EventArgs e)
        {
            if (Dock == DockStyle.None)
                return;

            if(Content.Controls.Count == 0)
            {
                if (!Collapsed)
                    Collapsed = true;

                CollapseButton.Enabled = false;
                Text = Texts.Layout.Empty;
                return;
            }

            if(CollapseButton.Enabled == false)
            {
                CollapseButton.Enabled = true;
                Collapsed = false;
            }

            Text = string.Join(
                ", ",
                Content.Controls.OfType<YaTabPage>().Select(tp => tp.Text).ToArray()
                );
        }
    }
}

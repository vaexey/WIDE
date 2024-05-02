using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Controls
{
    // https://stackoverflow.com/questions/5569284/how-do-i-change-background-colour-of-tab-control-in-winforms
    public class StyledTabControl : TabControl
    {
        public new TabDrawMode DrawMode
        {
            get
            {
                return TabDrawMode.OwnerDrawFixed;
            }
            set
            {
                // No you dont.
            }
        }

        public StyledTabControl()
        {
            base.DrawMode = TabDrawMode.OwnerDrawFixed;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        private struct TabItemInfo
        {
            public Color BackColor;
            public Rectangle Bounds;
            public Font? Font;
            public Color ForeColor;
            public int Index;
            public DrawItemState State;

            public TabItemInfo(DrawItemEventArgs e)
            {
                BackColor = e.BackColor;
                ForeColor = e.ForeColor;
                Bounds = e.Bounds;
                Font = e.Font;
                Index = e.Index;
                State = e.State;
            }
        }

        private Dictionary<int, TabItemInfo> _tabItemStateMap = new Dictionary<int, TabItemInfo>();
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (!_tabItemStateMap.ContainsKey(e.Index))
            {
                _tabItemStateMap.Add(e.Index, new TabItemInfo(e));
            }
            else
            {
                _tabItemStateMap[e.Index] = new TabItemInfo(e);
            }
        }

        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;

        // Cache context to avoid repeatedly re-creating the object.
        // WM_PAINT is called frequently so it's better to declare it as a member.
        private BufferedGraphicsContext _bufferContext = BufferedGraphicsManager.Current;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PAINT:
                    {
                        // Let system do its thing first.
                        base.WndProc(ref m);

                        // Custom paint Tab items.
                        HandlePaint(ref m);

                        break;
                    }
                case WM_ERASEBKGND:
                    {
                        if (DesignMode)
                        {
                            // Ignore to prevent flickering in DesignMode.
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    }
                default:
                    base.WndProc(ref m);
                    break;
            }
        }


        private Color _backColor = Color.FromArgb(31, 31, 31);
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        public Color TitleColor { get; set; } = SystemColors.ControlText;

        private void HandlePaint(ref Message m)
        {
            using (var g = Graphics.FromHwnd(m.HWnd))
            {
                SolidBrush backBrush = new SolidBrush(BackColor);
                Rectangle r = ClientRectangle;
                using (var buffer = _bufferContext.Allocate(g, r))
                {
                    if (Enabled)
                    {
                        buffer.Graphics.FillRectangle(backBrush, r);
                    }
                    else
                    {
                        buffer.Graphics.FillRectangle(backBrush, r);
                    }

                    // Paint items
                    foreach (int index in _tabItemStateMap.Keys)
                    {
                        DrawTabItemInternal(buffer.Graphics, _tabItemStateMap[index]);
                    }

                    buffer.Render();
                }
                backBrush.Dispose();
            }
        }


        private void DrawTabItemInternal(Graphics gr, TabItemInfo tabInfo)
        {
            /* Uncomment the two lines below to have each TabItem use the same height.
            ** The selected TabItem height will be slightly taller
            ** which makes unselected tabs float if you choose to 
            ** have a different BackColor for the TabControl background
            ** and your TabItem background. 
            */

            // int fullHeight = _tabItemStateMap[this.SelectedIndex].Bounds.Height;
            // tabInfo.Bounds.Height = fullHeight;

            SolidBrush backBrush = new SolidBrush(BackColor);

            // Paint selected. 
            // You might want to choose a different color for the 
            // background or the text.
            using (var titleBrush = new SolidBrush(TitleColor))
            {
                if ((tabInfo.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    gr.FillRectangle(backBrush, tabInfo.Bounds);
                    gr.DrawString(TabPages[tabInfo.Index].Text, tabInfo.Font,
                        titleBrush, tabInfo.Bounds);
                }
                // Paint unselected.
                else
                {
                    gr.FillRectangle(backBrush, tabInfo.Bounds);
                    gr.DrawString(TabPages[tabInfo.Index].Text, tabInfo.Font,
                        titleBrush, tabInfo.Bounds);
                }
            }

            backBrush.Dispose();
        }
    }
}

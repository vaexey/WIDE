using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaTabControl;

namespace WIDE.View.Layout
{
    public class LayoutContainer : Panel
    {
        public ObservableCollection<LayoutView> ViewControls { get; } = new();
        protected Dictionary<LayoutView, LayoutChild?> ViewControlOwners { get; } = new();

        public IEnumerable<LayoutChild> LayoutChildren =>
            Controls.OfType<LayoutChild>();

        public LayoutChild ChildLeft { get; set; } = new();
        public LayoutChild ChildCenter { get; set; } = new();
        public LayoutChild ChildRight { get; set; } = new();
        public LayoutChild ChildBottom { get; set; } = new();

        protected LayoutChild? Hovering { get; set; } = null;
        protected LayoutChild? Resizing { get; set; } = null;
        protected Point HoveringPoint { get; set; } = Point.Empty;
        protected Rectangle HoveringEdge { get; set; } = Rectangle.Empty;

        protected int MaxSidePanelWidth => Width / 2 - 10;

        public LayoutContainer()
        {
            Controls.AddRange(new Control[] {
                ChildLeft,
                ChildRight,
                ChildBottom,
                ChildCenter
            });

            foreach (var child in LayoutChildren)
            {
                child.MouseMove += Child_MouseMove;
                child.MouseDown += Child_MouseDown;
                child.MouseUp += Child_MouseUp;

                child.PickerButtonClick += Child_PickerClick;

                if (child != ChildCenter)
                    child.Resize += Child_Resized;
            }

            MouseMove += Child_MouseMove;
            Resize += Child_Resized;

            ViewControls.CollectionChanged += ViewControls_CollectionChanged;

            ChildrenLayout();
        }

        private void ViewControls_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems != null)
                foreach (LayoutView ctl in e.NewItems)
                {
                    if (!ViewControlOwners.ContainsKey(ctl))
                        ViewControlOwners[ctl] = null;

                    ctl.MouseMove += delegate (object? s, MouseEventArgs e)
                    {
                        Child_MouseMove(ViewControlOwners[ctl], e);
                    };

                    ctl.Visible = false;
                    Controls.Add(ctl);
                }
        }

        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    base.OnControlAdded(e);

        //    if (e.Control is LayoutView lv)
        //        lv.MouseMove += Child_MouseMove;
        //}

        //protected override void OnControlRemoved(ControlEventArgs e)
        //{
        //    base.OnControlRemoved(e);

        //    if (e.Control is LayoutView lv)
        //        lv.MouseMove -= Child_MouseMove;
        //}

        protected void ChildrenLayout()
        {
            //ChildBottom.Size = new Size(100, 100);
            //ChildLeft.Size = new Size(100, 100);
            //ChildRight.Size = new Size(100, 100);
            //ChildCenter.Size = new Size(100, 100);

            ChildBottom.Dock = DockStyle.Bottom;
            ChildLeft.Dock = DockStyle.Left;
            ChildRight.Dock = DockStyle.Right;
            ChildCenter.Dock = DockStyle.None;

            //ChildCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        protected void Child_Resized(object? sender, EventArgs e)
        {
            ChildCenter.Top = 0;
            ChildCenter.Left = ChildLeft.Right;
            ChildCenter.Width = Width - (ChildLeft.Width + ChildRight.Width);
            ChildCenter.Height = Height - (ChildBottom.Height);

            foreach(var child in new List<LayoutChild>() { ChildLeft, ChildRight })
            {
                if (MaxSidePanelWidth <= child.CollapsedSize)
                    continue;

                if (child.Width > MaxSidePanelWidth)
                    child.Width = MaxSidePanelWidth;
            }
        }

        protected void Child_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is LayoutChild lc)
            {
                if (Hovering != null && Resizing == null)
                {
                    Resizing = Hovering;
                }
            }
        }
        protected void Child_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is LayoutChild lc)
            {
                if (Resizing != null)
                {
                    Hovering = null;
                    Resizing = null;
                }
            }
        }

        protected void Child_MouseMove(object? sender, MouseEventArgs e)
        {
            //BackColor = (Resizing is null) ? Color.White : Color.Red;

            var margin = 5;

            var mouse = e.Location;

            if(sender is LayoutChild lc)
            {
                mouse.Offset(lc.Location);

                if (Hovering != null && Resizing == null)
                {

                    if(mouse.X < 50)
                    {

                    }

                    if (!HoveringEdge.Contains(mouse))
                    {
                        Hovering = null;
                        //Resizing = null;

                        Cursor = Cursors.Default;

                        return;
                    }
                }

                if (Resizing != null)
                {
                    var deltaX = mouse.X - HoveringPoint.X;
                    var deltaY = mouse.Y - HoveringPoint.Y;
                    var newSize = Resizing.Size;
                    var edgeOffset = new Point(0, 0);

                    HoveringPoint = mouse;

                    switch (Resizing.Dock)
                    {
                        case DockStyle.Left:
                            newSize.Width += deltaX;
                            edgeOffset.Offset(deltaX, 0);

                            break;
                        case DockStyle.Right:
                            newSize.Width -= deltaX;
                            edgeOffset.Offset(deltaX, 0);

                            break;
                        case DockStyle.Top:
                            newSize.Height += deltaY;
                            edgeOffset.Offset(0, deltaY);

                            break;
                        case DockStyle.Bottom:
                            newSize.Height -= deltaY;
                            edgeOffset.Offset(0, deltaY);

                            break;
                        default:
                            throw new InvalidOperationException("Cannot resize a non-docked LayoutChild");
                    }

                    if(newSize.Width <= Resizing.MinimumUncollapsedSize || newSize.Height <= Resizing.MinimumUncollapsedSize)
                    {
                        return;
                    }

                    // TODO: vertical too
                    switch(Resizing.Dock)
                    {
                        case DockStyle.Left:
                        case DockStyle.Right:
                            if (newSize.Width >= MaxSidePanelWidth)
                                newSize.Width = MaxSidePanelWidth;

                            break;
                    }

                    Resizing.Size = newSize;
                    HoveringEdge.Offset(edgeOffset);

                    return;
                }

                if (Hovering != null)
                {

                    return;
                }

                var edge = Rectangle.Empty;

                foreach (var child in LayoutChildren)
                {
                    if (child.Dock == DockStyle.None || child.Collapsed)
                        continue;

                    switch (child.Dock)
                    {
                        case DockStyle.Left:
                            edge = new(child.Left + child.Width - margin, child.Top, 2 * margin, child.Height);
                            break;
                        case DockStyle.Right:
                            edge = new(child.Left - margin, child.Top, 2 * margin, child.Height);
                            break;
                        case DockStyle.Top:
                            break;
                        case DockStyle.Bottom:
                            edge = new(child.Left, child.Top - margin, child.Width, 2 * margin);
                            break;
                        default:
                            continue;
                    }

                    if(edge.Contains(mouse))
                    {
                        Cursor = ((int)child.Dock >= 3) ? Cursors.VSplit : Cursors.HSplit;

                        Hovering = child;
                        HoveringPoint = mouse;
                        HoveringEdge = edge;

                        break;
                    }

                    edge = Rectangle.Empty;
                }

                if (edge.IsEmpty)
                    Cursor = Cursors.Default;
            }
        }

        protected void Child_PickerClick(object? sender, EventArgs e)
        {
            if(sender is LayoutChild lc)
            {
                var lpf = new LayoutPickerForm();

                lpf.Entries = ViewControls.Select(ctl =>
                {
                    return new LayoutPickerEntry(ctl, lc)
                    {
                        Owner = ViewControlOwners[ctl]
                    };
                }).ToList();

                if (lpf.ShowDialog(this) != DialogResult.OK)
                    return;

                foreach (var ent in lpf.Modified)
                {
                    SetViewOwner(ent.Control, ent.Owner);
                }
            }
        }

        public void SetViewOwner(LayoutView ctl, LayoutChild? newOwner)
        {
            var oldOwner = ViewControlOwners[ctl];
            if (newOwner == oldOwner)
            {
                return;
            }

            if(newOwner != null && oldOwner != null)
            {
                // swap owner
                SetViewOwner(ctl, null);
            }

            if (newOwner == null)
            {
                ctl.Parent = this;
                ViewControlOwners[ctl] = null;

                return;
            }

            ctl.Parent = newOwner.Content;
            ViewControlOwners[ctl] = newOwner;
        }
    }
}

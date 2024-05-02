using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.Register;
using static ScintillaNET.Style;

namespace WIDE.View.CPU
{
    internal class CpuElementControl : GroupBox
    {
        protected PictureBox dragButton;
        protected Point? dragPreviousLocation = null;

        protected Label statusLabel;

        public EventHandler MetaPositionChanged = delegate { };

        public ArchBlock Block { get; set; }

        private bool _draggable = false;
        public bool Draggable {
            get => _draggable;
            set
            {
                _draggable = value;

                dragButton.Cursor = value ? Cursors.Hand : Cursors.No;
            }
        }

        public CpuElementControl(ArchBlock block)
        {
            Block = block;

            Font = Styles.FontMonospace(Font.Size);

            // DRAG
            dragButton = new()
            {
                Width = 16,
                Height = 16,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Bottom,
                Image = Resources.t1_50,
            };

            Draggable = true;

            dragButton.MouseDown += dragButton_MouseDown;
            dragButton.MouseUp += dragButton_MouseUp;
            dragButton.MouseMove += dragButton_MouseMove;

            MouseMove += dragButton_MouseMove;
            MouseUp += dragButton_MouseUp;

            Controls.Add(dragButton);

            //STATUS
            statusLabel = new()
            {
                Top = 15,
                Left = 15,
                Text = "",
                Font = Styles.FontSans(Font.Size)
            };

            Controls.Add(statusLabel);
        }
    
        public void UpdateText()
        {
            var status = ArchBlockPrinter.GetStatusString(Block);
            if (statusLabel.Text != status)
                statusLabel.Text = status;

            if(Text != Block.Meta.Title)
                Text = Block.Meta.Title;
        }

        public void UpdatePosition()
        {
            var meta = Block.Meta;

            if (dragPreviousLocation == null)
            {
                Left = meta.X;
                Top = meta.Y;
                Width = meta.Width;
                Height = meta.Height;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            dragButton.Left = Width - 16 -3;
            dragButton.Top = Height - 16 -3;

            base.OnResize(e);
        }
        protected void dragButton_MouseDown(object? sender, MouseEventArgs e)
        {
            if(Draggable)
            {
                dragPreviousLocation = e.Location;
                Cursor = Cursors.SizeAll;
            }
        }

        protected void dragButton_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!Draggable || dragPreviousLocation == null)
                return;

            var loc = Location;
            loc.Offset(
                e.Location.X - dragPreviousLocation.Value.X,
                e.Location.Y - dragPreviousLocation.Value.Y
            );

            Location = bindToParent(loc);
        }

        protected void dragButton_MouseUp(object? sender, MouseEventArgs e)
        {
            Block.Meta.X = Location.X;
            Block.Meta.Y = Location.Y;

            dragPreviousLocation = null;
            Cursor = Cursors.Default;

            MetaPositionChanged(this, new EventArgs());
        }

        protected bool isInBounds(Point p)
        {
            return 
                p.X > 0 && 
                p.Y > 0 &&
                p.X < (Parent.Width - Width) &&
                p.Y < (Parent.Height - Height);
        }

        protected Point bindToParent(Point p)
        {
            return new Point(
                Math.Max(0, Math.Min(p.X, Parent.Width - Width)),
                Math.Max(0, Math.Min(p.Y, Parent.Height - Height))
            );
        }
    }
}

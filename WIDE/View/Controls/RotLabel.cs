using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Controls
{
    public class RotLabel : Label
    {
        protected int _rotationAngle = 0;
        public int RotationAngle
        {
            get => _rotationAngle;
            set
            {
                if (value % 90 != 0) throw new Exception("RotationAngle must be divisible by 90 degrees");

                _rotationAngle = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(RotationAngle == 0)
            {
                base.OnPaint(e);
                return;
            }

            var dimSwap = RotationAngle % 180 != 0;

            var size = e.ClipRectangle.Size;
            var newSize = dimSwap ? new Size(size.Height, size.Width) : size;

            var bmp = new Bitmap(newSize.Width, newSize.Height);
            var graph = Graphics.FromImage(bmp);

            graph.Clear(BackColor);

            //base.OnPaint(new PaintEventArgs(
            //        graph,
            //        new Rectangle(new Point(0,0), newSize)
            //    ));
            using(var foreBrush = new SolidBrush(ForeColor))
            {
                graph.DrawString(Text, Font, foreBrush, 0, 0);
            }

            switch (RotationAngle)
            {
                case 90:
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 180:
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 270:
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }

            e.Graphics.DrawImage(
                bmp,
                new Point(0,0));

            bmp.Dispose();
            graph.Dispose();
        }
    }
}

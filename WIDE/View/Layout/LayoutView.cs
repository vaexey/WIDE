using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YaTabControl;

namespace WIDE.View.Layout
{
    public class LayoutView : YaTabPage
    {
        protected MainParentForm MainForm => (MainParentForm)ParentForm;

        protected Image? tabImage;
        public Image? TabImage
        {
            get => tabImage;
            set
            {
                tabImage = value;

                if(value is null)
                {
                    ImageIndex = -1;
                    return;
                }

                AppendTabImageToParent();
            }
        }

        private void AppendTabImageToParent()
        {
            if (Parent is YaTabControl.YaTabControl ytc)
            {
                if (ytc.ImageList is null)
                    return;

                if (ImageIndex == -1)
                {
                    ytc.ImageList.Images.Add(TabImage);
                    ImageIndex = ytc.ImageList.Images.Count - 1;
                }
                else
                {
                    ytc.ImageList.Images[ImageIndex] = TabImage;
                }
            }
        }

        public LayoutView() : base()
        {
            
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (TabImage != null && ImageIndex == -1)
                AppendTabImageToParent();

            MainForm.LayoutReady += mainForm_Ready;
        }

        private void mainForm_Ready(object? sender, EventArgs e)
        {
            OnMainFormReady(e);
        }

        protected virtual void OnMainFormReady(EventArgs e)
        {
            //
        }
    }
}

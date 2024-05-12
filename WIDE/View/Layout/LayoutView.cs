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
        private bool onMainFormReadyFired = false;

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

            //OnMainFormReady();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            //OnMainFormReady();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            OnMainFormReady();
        }

        protected void OnMainFormReady()
        {
            if (TabImage != null && ImageIndex == -1)
                AppendTabImageToParent();

            if (onMainFormReadyFired)
                return;
            onMainFormReadyFired = true;

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

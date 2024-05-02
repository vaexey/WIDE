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

        public LayoutView() : base()
        {

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

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

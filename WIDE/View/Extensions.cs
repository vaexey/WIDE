using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View
{
    public static class Extensions
    {
        //public static ToolTip GetToolTipRecursive(this Control ctl)
        //{
        //    if(ctl is MainParentForm form)
        //    {
        //        var tooltip = form.tool

        //        if (tooltip is not null)
        //            return tooltip;
        //    }
        //    else 
        //    {
        //        if(ctl.Parent is not null)
        //            return ctl.Parent.GetToolTipRecursive();
        //    }

        //    throw new KeyNotFoundException("Parent form does not contain ToolTip object.");
        //}

        public static void SetToolTip(this Control ctl, Control target, string tip)
        {
            if(ctl is MainParentForm form)
            {
                form.ToolTipControl.SetToolTip(target, tip);
            }
            else if(ctl.Parent is not null)
            {
                ctl.Parent.SetToolTip(target, tip);
            }
        }

        public static void SetToolTip(this Control target, string tip)
        {
            target.SetToolTip(target, tip);
        }

        public static int MeasureRight(this Label lbl)
        {
            return lbl.Left + TextRenderer.MeasureText(lbl.Text, lbl.Font).Width;
        }

        public static int MeasureBottom(this Label lbl)
        {
            return lbl.Top + TextRenderer.MeasureText(lbl.Text, lbl.Font).Height;
        }
    }
}

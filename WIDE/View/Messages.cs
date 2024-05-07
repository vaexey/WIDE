using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.Controller;
using WIDE.View.Utility;

namespace WIDE.View
{
    public class Messages
    {
        public static void Show(string title, string message, bool error = false)
        {
            //MessageBox.Show(ex.Message, Texts.Global.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            var msf = new MessageForm(title, message, error);

            msf.ShowDialog();

            msf.Dispose();
        }

        public static void ShowException(UserFriendlyException ex)
        {
            Show(
                // TOOD: translate title
                ex.Category ?? "Message", 
                ex.Message, 
                ex.Error
                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.Register;

namespace WIDE.View.CPU
{
    public class ArchBlockPrinter
    {
        public static string GetStatusString(ArchBlock block)
        {
            var lv = block.GetLive();

            if (lv is null)
            {
                return "View will be available when an emulator is created.";
            }
            else if (lv is LiveRegister reg)
            {
                return "REG!";
            }
            else
            {
                return "This block does not have a view available.";
            }
        }
    }
}

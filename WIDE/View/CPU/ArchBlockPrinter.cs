using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.ALU;
using WIDEToolkit.Emulator.Blocks.MemHandler;
using WIDEToolkit.Emulator.Blocks.Register;

namespace WIDE.View.CPU
{
    public class ArchBlockPrinter
    {
        public static string GetStatusString(ArchBlock block)
        {
            var lv = block.GetLive();

            if(block is ALU alu)
            {
                return Texts.Emulator.BlockDescALU;
            }else if (lv is null)
            {
                return Texts.Emulator.BlockDescNoLive;
            }
            else if (lv is LiveRegister reg)
            {
                return reg.Data.ToUInt64().ToString();
            }
            else if(lv is LiveMemHandler memh)
            {
                return memh.Memory.GetSize().ToString();
            }
            else
            {
                return Texts.Emulator.BlockDescUndefined;
            }
        }
    }
}

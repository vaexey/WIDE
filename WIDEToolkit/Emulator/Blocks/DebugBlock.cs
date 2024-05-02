using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks
{
    public class DebugBlock : ArchBlock<LiveDebugBlock>
    {
        public override void CreateLive(Architecture arch)
        {
            Live = new LiveDebugBlock(
                new Signal[] { 
                    new Signal("__stop", this)
                }
            );
        }
    }
}

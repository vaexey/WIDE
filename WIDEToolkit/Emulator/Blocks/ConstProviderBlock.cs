using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Live;

namespace WIDEToolkit.Emulator.Blocks
{
    public class ConstProviderBlock : ArchBlock<LiveConstProviderBlock>
    {
        public override void CreateLive(Architecture arch)
        {
            Live = new();
        }
    }
}

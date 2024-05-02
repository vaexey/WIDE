using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Live
{
    public class LiveDebugBlock : LiveBlock
    {
        public LiveDebugBlock(Signal[] s)
        {
            Signals = s;
        }
    }
}

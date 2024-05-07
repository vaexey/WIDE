using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Blocks.MemBlock
{
    public class MemBlueprint
    {
        public string? DivisionName { get; set; } = null;
        public int Size { get; set; } = 0;
        public int WordSize { get; set; } = 8;

        public Type MemoryType { get; set; } = typeof(SingleMemory);
    }
}

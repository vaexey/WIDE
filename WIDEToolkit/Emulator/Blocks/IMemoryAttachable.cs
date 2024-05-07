using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Blocks
{
    public interface IMemoryAttachable
    {
        public string GetDivisionName();
        public void AttachMemory(Memory memory);
    }
}

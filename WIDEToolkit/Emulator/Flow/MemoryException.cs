using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Flow
{
    public class MemoryException : EmulatorException
    {
        public MemoryException(string message) : base(message) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Data
{
    public class MemoryException : EmulatorException
    {
        public MemoryException(string message) : base(message) { }
    }
}

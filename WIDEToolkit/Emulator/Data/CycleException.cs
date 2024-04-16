using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Data
{
    public class CycleException : EmulatorException
    {
        public CycleException(string message) : base(message) { }
    }
}

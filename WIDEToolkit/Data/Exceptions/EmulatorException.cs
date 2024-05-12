using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Data.Exceptions
{
    public class EmulatorException : Exception
    {
        public EmulatorException(string message) : base(message) { }
    }
}

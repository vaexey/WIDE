using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Flow
{
    public enum EndpointType
    {
        BUS = 0x1,
        REGISTER = 0x2,
        DISJOINTED_RO = 0x10,
        DISJOINTED_WO = 0x11
    }
}

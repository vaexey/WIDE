using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public enum RegisterSignalMode
    {
        NOOP = 0,
        
        LOAD = 0x1,
        STORE = 0x2,

        // TODO
        MERGE = 0x4,

        //SUM = 0x4

        //LOAD_IMM = LOAD & IMMEDIATE,
        //STORE_IMM = STORE & IMMEDIATE,

        //IMMEDIATE = 0x8000000
    }
}

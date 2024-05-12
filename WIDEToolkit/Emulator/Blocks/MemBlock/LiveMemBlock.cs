using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Emulator.Blocks.MemBlock
{
    public class LiveMemBlock : LiveBlock
    {
        public List<Memory> Divisions { get; }

        public LiveMemBlock(List<Memory> divisions)
        {
            Divisions = divisions;
        }

        public Memory Get(string divName)
        {
            var mem = Divisions.Find(e => e.DivisionName == divName);

            if (mem is null)
                throw new FlowException($"Memory division {divName} does not exist.");

            return mem;
        }
    }
}

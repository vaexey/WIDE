using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;

namespace WIDEToolkit.Emulator.Flow
{
    public class Signal
    {
        public string Name { get; }
        
        public ArchBlock Owner { get; }

        public Signal(string name, ArchBlock owner)
        {
            Name = name;
            Owner = owner;
        }
    }
}

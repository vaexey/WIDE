using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Flow
{
    public class Endpoint
    {
        public string Name { get; set; }
        public ArchBlock Owner { get; set; }

        public Endpoint(string name, ArchBlock block)
        {
            Name = name;
            Owner = block;
        }

        public WORD ReadEndpoint(Architecture arch, int width)
        {
            return Owner.GetLive().ReadEndpoint(arch, this, width);
        }


        public void WriteEndpoint(Architecture arch, WORD value)
        {
            Owner.GetLive().WriteEndpoint(arch, this, value);
        }
    }
}

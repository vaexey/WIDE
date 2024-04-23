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
        public EndpointType Type { get; set; }
        public ArchBlock Owner { get; set; }

        public Endpoint(string name, EndpointType type, ArchBlock block)
        {
            Name = name;
            Type = type;
            Owner = block;
        }

        public WORD ReadEndpoint(int width)
        {
            return Owner.GetLive().ReadEndpoint(this, width);
        }


        public void WriteEndpoint(WORD value)
        {
            Owner.GetLive().WriteEndpoint(this, value);
        }
    }
}

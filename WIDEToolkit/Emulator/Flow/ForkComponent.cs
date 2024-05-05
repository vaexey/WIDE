using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Flow
{
    public class ForkComponent
    {
        public string Endpoint { get; }
        
        public int Multiplier { get; }

        public ForkComponent(string endpoint, int multiplier = 1)
        {
            Endpoint = endpoint;
            Multiplier = multiplier;
        }

        public int GetComponent(Architecture arch)
        {
            return (int)arch.GetEndpoint(Endpoint).ReadEndpoint(64).ToUInt64() * Multiplier;
        }
    }
}

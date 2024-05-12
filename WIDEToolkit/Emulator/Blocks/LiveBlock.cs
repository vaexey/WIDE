using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks
{
    public abstract class LiveBlock
    {
        public Signal[] Signals { get; protected set; } = new Signal[0];

        public Endpoint[] Endpoints { get; protected set; } = new Endpoint[0];
        
        public virtual void ExecuteSignal(Architecture arch, Signal signal)
        {
            throw new FlowException($"Targeted block {GetType()} does not support signals.");
        }

        public virtual WORD ReadEndpoint(Endpoint ep, int width)
        {
            throw new FlowException($"Targeted block {GetType()} does not support reading from endpoint.");
        }

        public virtual void WriteEndpoint(Endpoint ep, WORD value)
        {
            throw new FlowException($"Targeted block {GetType()} does not support writing to endpoint.");
        }
        public virtual void Commit() { }
    }
}

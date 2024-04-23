using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks
{
    public abstract class LiveBlock
    {
        public Signal[] Signals { get; protected set; } = new Signal[0];

        public Endpoint[] Endpoints { get; protected set; } = new Endpoint[0];
        
        public virtual void ExecuteSignal(Architecture arch, Signal signal)
        {
            return;
        }

        public virtual WORD ReadEndpoint(Endpoint ep, int width)
        {
            return WORD.Zero(width);
        }

        public virtual void WriteEndpoint(Endpoint ep, WORD value)
        {
            return;
        }
        public virtual void Commit() { }
    }
}

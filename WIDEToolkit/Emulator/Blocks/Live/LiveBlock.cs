using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Live
{
    public abstract class LiveBlock
    {
        public Signal[] Signals { get; protected set; } = new Signal[0];

        public Endpoint[] Endpoints { get; protected set; } = new Endpoint[0];

        public bool Dirty { get; protected set; } = false;

        public virtual void ExecuteSignal(Architecture arch, Signal signal)
        {
            return;
        }

        public virtual WORD ReadEndpoint(Architecture arch, Endpoint ep, int width)
        {
            return WORD.Zero(width);
        }

        public virtual void WriteEndpoint(Architecture arch, Endpoint ep, WORD value)
        {
            return;
        }
        public virtual void SetDirty()
        {
            Dirty = true;
        }
        public virtual void SetNotDirty()
        {
            Dirty = false;
        }
    }
}

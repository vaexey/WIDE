using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.MemHandler
{
    public class LiveMemHandler : LiveBlock, IMemoryAttachable
    {
        public Memory Memory { get; set; }
        public MemHandler Parent { get; }

        public string Division { get; }
        public string AddressEndpoint { get; }

        public LiveMemHandler(MemHandler parent, string addressEndpoint, string division, IEnumerable<Signal> signals, Memory? mem = null)
        {
            Parent = parent;
            AddressEndpoint = addressEndpoint;
            Division = division;
            Signals = signals.ToArray();

            Memory = mem ?? new SingleMemory(0);
        }

        public override void ExecuteSignal(Architecture arch, Signal signal)
        {
            MemHandlerSignal ms;

            try
            {
                ms = (MemHandlerSignal)signal;
            }
            catch
            {
                throw new FlowException($"Unrecognized signal {signal.Name} in memory handler {Parent.BaseName}");
            }

            if(ms.Type == MemHandlerSignalType.READ)
            {
                var aep = arch.GetEndpoint(AddressEndpoint);
                var dep = arch.GetEndpoint(ms.Endpoint);

                var addr = aep.ReadEndpoint(Parent.AddressWidth).ToUInt64();

                var rd = Memory.Read((int)addr);

                dep.WriteEndpoint(rd);

                return;
            }

            if(ms.Type == MemHandlerSignalType.WRITE)
            {
                var aep = arch.GetEndpoint(AddressEndpoint);
                var dep = arch.GetEndpoint(ms.Endpoint);

                var addr = aep.ReadEndpoint(Parent.AddressWidth).ToUInt64();

                var wt = dep.ReadEndpoint(Memory.GetWordSize());
                Memory.Write((int)addr, wt);

                return;
            }
        }

        public string GetDivisionName() => Division;

        public void AttachMemory(Memory memory)
        {
            Memory = memory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Data;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class LiveALU : LiveBlock
    {
        public ALU Parent { get; }

        public LiveALU(ALU parent, IEnumerable<Signal> signals)
        {
            Parent = parent;

            Signals = signals.ToArray();
        }

        public override void ExecuteSignal(Architecture arch, Signal signal)
        {
            ALUSignal ls;

            try
            {
                ls = (ALUSignal)signal;
            }
            catch
            {
                throw new FlowException($"Unrecognized signal {signal.Name} in ALU {Parent.BaseName}");
            }

            var sources = ls.SourceEndpoints.Select(
                name => arch.GetEndpoint(name)).ToArray();
            var dests = ls.DestEndpoints.Select(
                name => arch.GetEndpoint(name)).ToArray();

            ls.Operation.Execute(sources, dests);
        }
    }
}

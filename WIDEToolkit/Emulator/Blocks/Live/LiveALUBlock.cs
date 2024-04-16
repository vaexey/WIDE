using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Live
{
    public class LiveALUBlock : LiveBlock
    {
        public override void ExecuteSignal(Architecture arch, Signal signal)
        {
            ALUSignal als;

            try
            {
                als = (ALUSignal)signal;
            }
            catch
            {
                throw new FlowException($"Unrecognized signal {signal.Name} in alu");
            }

            var ep1 = arch.GetEndpoint(als.Endpoint1);
            var ep2 = arch.GetEndpoint(als.Endpoint2);
            var ep3 = arch.GetEndpoint(als.Endpoint3);

            if(als.Operation == ALUBlock.ALUOperation.PASS)
            {
                var rd1 = ep1.ReadEndpoint(arch, als.Width);

                ep3.WriteEndpoint(arch, rd1);

                return;
            }

            if(als.Operation == ALUBlock.ALUOperation.ADD)
            {
                var rd1 = ep1.ReadEndpoint(arch, als.Width);
                var rd2 = ep2.ReadEndpoint(arch, als.Width);

                ep3.WriteEndpoint(arch, rd1 + rd2);

                return;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Live
{
    public class LiveConstProviderBlock : LiveBlock
    {
        public override WORD ReadEndpoint(Endpoint ep, int width)
        {
            if (!ep.Name.StartsWith("__const_"))
                throw new FlowException($"Non-const endpoint {ep.Name} was read from const provider");

            var str = ep.Name.Remove(0, "__const_".Length);

            try
            {
                var i = ulong.Parse(str);

                return WORD.FromUInt64(i);
            }
            catch
            {
                throw new FlowException($"Could not parse constant {ep.Name}");
            }
        }

        public override void WriteEndpoint(Endpoint ep, WORD value)
        {
            throw new FlowException($"Value was written to const provider at {ep.Name}");
        }
    }
}

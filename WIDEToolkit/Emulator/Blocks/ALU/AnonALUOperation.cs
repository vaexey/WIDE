using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class AnonALUOperation : ALUOperation
    {
        public Action<Endpoint[], Endpoint[]> Method { get; }

        protected AnonALUOperation(Action<Endpoint[], Endpoint[]> method)
        {
            Method = method;
        }
        public static AnonALUOperation From(Action<Endpoint[], Endpoint[]> method)
        {
            return new AnonALUOperation(method);
        }

        public override void Execute(Endpoint[] sources, Endpoint[] destinations)
        {
            Method(sources, destinations);
        }

        public static AnonALUOperation NOOP()
        {
            return From((a, b) => { });
        }

        public static AnonALUOperation Sum(int width)
        {
            return From((s, d) =>
            {
                var words = s.Select(ep => ep.ReadEndpoint(width));

                var sum = WORD.Zero(width);

                foreach (var w in words)
                    sum.Add(w);

                foreach (var ep in d)
                    ep.WriteEndpoint(sum);
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Flow
{
    public class ForkEvaluator
    {
        public ForkComponent[] Components { get; }

        public ForkEvaluator(ForkComponent[]? components = null)
        {
            Components = components ?? Array.Empty<ForkComponent>();
        }

        public virtual int GetForkValue(Architecture arch)
        {
            int fork = 0;

            foreach (var component in Components)
                fork += component.GetComponent(arch);

            return fork;
        }
    }
}

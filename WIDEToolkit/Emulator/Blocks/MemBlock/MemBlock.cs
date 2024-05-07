using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace WIDEToolkit.Emulator.Blocks.MemBlock
{
    public class MemBlock : ArchBlock<LiveMemBlock>
    {
        public List<MemBlueprint> MemoryDivisions { get; set; } = new();

        public override void CreateLive(Architecture arch)
        {
            var divisions = new List<Memory>();

            foreach(var md in MemoryDivisions)
            {
                object? created = Activator.CreateInstance(md.MemoryType, md.Size, md.WordSize);

                if(created is Memory mem)
                {
                    mem.DivisionName = md.DivisionName;

                    divisions.Add(mem);
                    continue;
                }

                throw new FlowException($"Memory division {md.DivisionName} has invalid type {md.MemoryType}");
            }

            Live = new(divisions);
        }
    }
}

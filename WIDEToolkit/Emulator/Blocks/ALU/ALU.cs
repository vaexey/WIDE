using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class ALU : ArchBlock<LiveALU>
    {
        public List<ALUSignalBlueprint> Signals = new();

        public string BaseName { get; set; } = "ALU";

        public override void CreateLive(Architecture arch)
        {
            var sigs = Signals.Select(bp =>
            {
                var regex = bp.NameRegex.Match(BaseName).Groups.Values.Select(g => g.Value).ToArray();

                return new ALUSignal(
                    string.Format(bp.NameFormat, regex),
                    this,
                    bp.SourceEndpointFormats.Select(ef =>
                        string.Format(ef, regex)).ToArray(),
                    bp.DestEndpointFormats.Select(ef =>
                        string.Format(ef, regex)).ToArray(),
                    bp.Operation
                );
            });

            Live = new LiveALU(this, sigs);
        }
    }
}

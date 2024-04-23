using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.ALU
{
    public class ALU : ArchBlock<LiveALU>
    {
        [Browsable(true)]
        [DisplayName("Base name")]
        [Description("Register base name. Used in custom formats to create signals and internal emulator endpoints.")]
        [Category("General")]
        public string BaseName { get; set; } = "ALU";

        [Browsable(true)]
        [DisplayName("Signals")]
        [Description("ALU signals set.")]
        [Category("Flow")]
        public List<ALUSignalBlueprint> Signals { get; set; } = new();

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

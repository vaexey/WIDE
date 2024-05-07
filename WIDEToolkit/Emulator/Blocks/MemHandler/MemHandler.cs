using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Blocks.MemHandler
{
    public class MemHandler : ArchBlock<LiveMemHandler>
    {
        [Browsable(true)]
        [DisplayName("Base name")]
        [Description("Register base name. Used in custom formats to create signals and internal emulator endpoints.")]
        [Category("General")]
        public string BaseName { get; set; } = "RAM";
        public string DivisionFormat { get; set; } = "{1}";

        public Regex NameRegex { get; set; } = new Regex("(^.*$)");

        public string AddresEndpointFormat { get; set; } = "A";
        public int AddressWidth { get; set; } = 64;

        public string? WriteSignalFormat { get; set; } = "write_{1}";
        public string? ReadSignalFormat { get; set; } = "read_{1}";

        public string WriteEndpointFormat { get; set; } = "{1}_in";
        public string ReadEndpointFormat { get; set; } = "{1}_out";

        public override void CreateLive(Architecture arch)
        {
            var sigs = new List<MemHandlerSignal>();
            
            var regex = NameRegex.Match(BaseName).Groups.Values.Select(g => g.Value).ToArray();

            if (WriteSignalFormat is not null)
            {
                sigs.Add(new(
                    string.Format(WriteSignalFormat, regex),
                    string.Format(WriteEndpointFormat, regex),
                    this,
                    MemHandlerSignalType.WRITE
                ));
            }

            if (ReadSignalFormat is not null)
            {
                sigs.Add(new(
                    string.Format(ReadSignalFormat, regex),
                    string.Format(ReadEndpointFormat, regex),
                    this,
                    MemHandlerSignalType.READ
                ));
            }

            Live = new LiveMemHandler(
                this,
                string.Format(AddresEndpointFormat, regex),
                string.Format(DivisionFormat, regex),
                sigs,
                null
            );
        }
    }
}

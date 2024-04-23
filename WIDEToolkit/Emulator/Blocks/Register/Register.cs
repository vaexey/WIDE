using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class Register : ArchBlock<LiveRegister>
    {
        [Browsable(true)]
        [DisplayName("Base name")]
        [Description("Register base name. Used in custom formats to create signals and internal emulator endpoints.")]
        [Category("General")]
        public string BaseName { get; set; } = "R";

        [Browsable(true)]
        [DisplayName("Width")]
        [Description("Register width specified in bits.")]
        [Category("Data")]
        public int Width { get; set; } = 8;

        [Browsable(true)]
        [DisplayName("Divisions")]
        [Description("Register division set.")]
        [Category("Data")]
        public List<RegisterDivisionBlueprint> Divisions { get; set; } = new();

        public bool AdjustRegisterToAddressSize { get; set; } = false;

        public override void CreateLive(Architecture arch)
        {
            var sigs = new List<RegisterSignal>();
            var endps = new List<Endpoint>();
            var divs = new List<LiveRegister.LiveRegisterDivision>();

            for (int i = 0; i < Divisions.Count; i++)
            {
                var sd = Divisions[i];

                int start = sd.Start;
                int end = sd.End;

                if (AdjustRegisterToAddressSize)
                {
                    start = sd.Start * arch.AddressWidth / Width;
                    end = sd.End * arch.AddressWidth / Width;
                }

                var regex = sd.NameRegex.Match(BaseName).Groups.Values.Select(g => g.Value).ToArray();


                Endpoint ep = new(
                    string.Format(sd.NameFormat, regex),
                    sd.EndpointType,
                    this
                );

                endps.Add(ep);

                divs.Add(new(
                    string.Format(sd.NameFormat, regex),
                    start,
                    end,
                    ep
                ));

                sigs.AddRange(
                    sd.Signals.Select(blu => new RegisterSignal(
                        string.Format(blu.NameFormat, regex),
                        string.Format(blu.EndpointFormat, regex),
                        this,
                        i,
                        blu.Mode
                    ))
                );
            }

            int width = Width;

            if (AdjustRegisterToAddressSize)
                width = arch.AddressWidth;

            Live = new(
                this,
                width,
                sigs,
                endps,
                divs
            );
        }
    }
}

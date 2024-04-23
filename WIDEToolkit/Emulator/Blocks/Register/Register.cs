using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class Register : ArchBlock<LiveRegister>
    {
        public int Width { get; set; } = 8;
        public List<RegisterDivisionBlueprint> Divisions { get; set; } = new();
            //new RegisterDivisionBlueprint[] { new() {
            //    //SignalIn = new(
            //    //    "BUS",
            //    //    "i_{0}"
            //    //),
            //    //SignalOut = new(
            //    //    "BUS",
            //    //    "o_{0}"
            //    //),
            //    Start = 0,
            //    End = 8,
            //} };

        public string BaseName { get; set; } = "R";

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

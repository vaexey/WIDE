using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks
{
    public class Register : ArchBlock<LiveRegister>
    {
        public class RegDivisionSignalDesc
        {
            public string EndpointFormat;
            public string SignalFormat;

            public RegDivisionSignalDesc(string endpointFormat, string signalFormat)
            {
                EndpointFormat = endpointFormat;
                SignalFormat = signalFormat;
            }
        }

        public class RegDivision
        {
            public string NameFormat = "{0}";
            public RegDivisionSignalDesc? SignalIn;
            public RegDivisionSignalDesc? SignalOut;
            public RegDivisionSignalDesc? SignalAdd;
            public Regex NameRegex = new Regex("^.*$");
            public int Start;
            public int End;
            public int Width { get => End - Start; }
        }

        public int Width { get; set; } = 8;
        public RegDivision[] Divisions { get; set; } = 
            new RegDivision[] { new() {
                SignalIn = new(
                    "BUS",
                    "i_{0}"
                ),
                SignalOut = new(
                    "BUS",
                    "o_{0}"
                ),
                Start = 0,
                End = 8,
            } };

        public string BaseName { get; set; } = "R";

        public bool AdjustRegisterToAddressSize { get; set; } = false;

        public override void CreateLive(Architecture arch)
        {
            var sigs = new List<RegisterSignal>();
            var endps = new List<Endpoint>();
            var divs = new List<LiveRegister.LiveRegisterDivision>();

            for (int i = 0; i < Divisions.Length; i++)
            {
                var sd = Divisions[i];

                int start = sd.Start;
                int end = sd.End;

                if(AdjustRegisterToAddressSize)
                {
                    start = sd.Start * arch.AddressWidth / Width;
                    end = sd.End * arch.AddressWidth / Width;
                }

                var regex = sd.NameRegex.Match(BaseName).Groups.Values.Select(g => g.Value).ToArray();

                divs.Add(new(
                    string.Format(sd.NameFormat, regex),
                    start,
                    end
                ));

                endps.Add(new(
                    string.Format(sd.NameFormat, regex),
                    this
                ));

                if (sd.SignalIn != null)
                    sigs.Add(new RegisterSignal(
                            string.Format(sd.SignalIn.SignalFormat, regex),
                            sd.SignalIn.EndpointFormat,
                            this,
                            i,
                            RegisterSignal.RegisterSignalMode.LOAD
                        ));

                if (sd.SignalOut != null)
                    sigs.Add(new RegisterSignal(
                            string.Format(sd.SignalOut.SignalFormat, regex),
                            sd.SignalOut.EndpointFormat,
                            this,
                            i,
                            RegisterSignal.RegisterSignalMode.STORE
                        ));

                if (sd.SignalAdd != null)
                    sigs.Add(new RegisterSignal(
                            string.Format(sd.SignalAdd.SignalFormat, regex),
                            sd.SignalAdd.EndpointFormat,
                            this,
                            i,
                            RegisterSignal.RegisterSignalMode.SUM
                        ));
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

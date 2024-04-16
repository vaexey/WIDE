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
    public class ALUBlock : ArchBlock<LiveALUBlock>
    {
        public enum ALUOperation
        {
            PASS = 0,
            ADD = 1,
        }

        public class ALUOperationDesc
        {
            public ALUOperation Operation;
            public string SignalFormat = "{1}";
            public string EndpointFormat1 = "{1}";
            public string EndpointFormat2 = "{1}";
            public string EndpointFormat3 = "{1}";

            public int Width = 64;
            public Regex NameRegex = new Regex("^(.*)$");
            public bool AdjustToAddressSize = false;
        }

        public ALUOperationDesc[] Operations = 
            new ALUOperationDesc[]{ };

        public string BaseName { get; set; } = "ALU";

        public override void CreateLive(Architecture arch)
        {
            var sigs = new List<ALUSignal>();

            foreach(var op in Operations)
            {
                var regex = op.NameRegex.Match(BaseName).Groups.Values.Select(g => g.Value).ToArray();

                sigs.Add(
                    new(
                        string.Format(op.SignalFormat, regex),
                        string.Format(op.EndpointFormat1, regex),
                        string.Format(op.EndpointFormat2, regex),
                        string.Format(op.EndpointFormat3, regex),
                        this,
                        op.Operation,
                        op.AdjustToAddressSize ?
                            arch.AddressWidth :
                            op.Width
                    )
                );
            }
        }
    }
}

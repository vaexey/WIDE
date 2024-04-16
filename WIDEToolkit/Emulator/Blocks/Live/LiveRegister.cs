using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Live
{
    public class LiveRegister : LiveBlock
    {
        public class LiveRegisterDivision
        {
            public string Name { get; }
            public int Start { get; }
            public int End { get; }
            public int Width { get => End - Start; }

            public LiveRegisterDivision(string name, int start, int end)
            {
                Name = name;
                Start = start;
                End = end;
            }
        }

        public Register Parent { get; }

        public WORD Data { get; }
        public int Width { get; }
        public LiveRegisterDivision[] Divisions { get; }
        
        public LiveRegister(Register parent, int width, IEnumerable<Signal> signals, IEnumerable<Endpoint> endps, IEnumerable<LiveRegisterDivision> divs)
        {
            Parent = parent;
            Width = width;
            Data = WORD.Zero(width);

            Signals = signals.ToArray();
            Endpoints = endps.ToArray();
            Divisions = divs.ToArray();

            // TODO: checking for valid register
        }

        public override void ExecuteSignal(Architecture arch, Signal signal)
        {
            RegisterSignal rs;

            try
            {
                rs = (RegisterSignal)signal;
            }
            catch
            {
                throw new FlowException($"Unrecognized signal {signal.Name} in register {Parent.BaseName}");
            }

            var ep = arch.GetEndpoint(rs.Endpoint);
            var div = Divisions[rs.DivisionIndex];

            if(rs.Mode == RegisterSignal.RegisterSignalMode.LOAD)
            {
                var rd = ep.Owner.GetLive().ReadEndpoint(arch, ep, div.Width);

                //Data[div.Start, div.Width] = rd;
                Data.Write(rd, div.Start, 0, div.Width);

                return;
            }
            
            if(rs.Mode == RegisterSignal.RegisterSignalMode.STORE)
            {
                var rd = Data.Slice(div.Start, div.End);

                ep.Owner.GetLive().WriteEndpoint(arch, ep, rd);

                return;
            }

            if(rs.Mode == RegisterSignal.RegisterSignalMode.SUM)
            {
                var a = Data.Slice(div.Start, div.End);
                var b = ep.Owner.GetLive().ReadEndpoint(arch, ep, div.Width);

                a.Add(b);

                Data.Write(a, div.Start, 0, div.Width);

                return;
            }
        }

        public override WORD ReadEndpoint(Architecture arch, Endpoint ep, int width)
        {
            var div = Divisions.Where(x => x.Name == ep.Name).First();

            if (div == null)
                throw new FlowException($"Unknown memory endpoint {ep.Name} in register {Parent.BaseName}");

            var rd = Data[div.Start, div.End];

            rd.ExtendTo(width);

            return rd;
        }

        public override void WriteEndpoint(Architecture arch, Endpoint ep, WORD value)
        {
            var div = Divisions.Where(x => x.Name == ep.Name).First();

            if (div == null)
                throw new FlowException($"Unknown memory endpoint {ep.Name} in register {Parent.BaseName}");

            Data[div.Start] = value[0, div.Width];

            SetDirty();
        }
    }
}

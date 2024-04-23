using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Blocks.Register
{
    public class LiveRegister : LiveBlock
    {
        public class LiveRegisterDivision
        {
            public string Name { get; }
            public int Start { get; }
            public int End { get; }
            public int Width { get => End - Start; }

            public Endpoint Endpoint { get; }

            public LiveRegisterDivision(string name, int start, int end, Endpoint endpoint)
            {
                Name = name;
                Start = start;
                End = end;
                Endpoint = endpoint;
            }
        }

        public Register Parent { get; }

        public WORD Data { get; }
        public WORD PendingData { get; }

        public int Width { get; }
        public LiveRegisterDivision[] Divisions { get; }

        public LiveRegister(Register parent, int width, IEnumerable<Signal> signals, IEnumerable<Endpoint> endps, IEnumerable<LiveRegisterDivision> divs)
        {
            Parent = parent;
            Width = width;
            Data = WORD.Zero(width);
            PendingData = WORD.Zero(width);

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

            if ((rs.Mode & RegisterSignalMode.LOAD) != 0)
            {
                var rd = ep.Owner.GetLive().ReadEndpoint(ep, div.Width);

                //Data[div.Start, div.Width] = rd;

                PendingData.Write(rd, div.Start, 0, div.Width);

                if(div.Endpoint.Type == EndpointType.BUS)
                    Data.Write(rd, div.Start, 0, div.Width);

                return;
            }

            if ((rs.Mode & RegisterSignalMode.STORE) != 0)
            {
                var rd = Data.Slice(div.Start, div.End);

                ep.Owner.GetLive().WriteEndpoint(ep, rd);

                return;
            }

            //if ((rs.Mode & RegisterSignalMode.SUM) != 0)
            //{
            //    var a = Data.Slice(div.Start, div.End);
            //    var b = ep.Owner.GetLive().ReadEndpoint(ep, div.Width);

            //    a.Add(b);

            //    PendingData.Write(a, div.Start, 0, div.Width);

            //    if (div.Endpoint.Type == EndpointType.BUS)
            //        Data.Write(a, div.Start, 0, div.Width);

            //    return;
            //}
        }

        public override WORD ReadEndpoint(Endpoint ep, int width)
        {
            var div = Divisions.Where(x => x.Name == ep.Name).First();

            if (div == null)
                throw new FlowException($"Unknown memory endpoint {ep.Name} in register {Parent.BaseName}");

            var rd = Data[div.Start, div.End];

            rd.ExtendTo(width);

            return rd;
        }

        public override void WriteEndpoint(Endpoint ep, WORD value)
        {
            var div = Divisions.Where(x => x.Name == ep.Name).First();

            if (div == null)
                throw new FlowException($"Unknown memory endpoint {ep.Name} in register {Parent.BaseName}");

            var dat = value[0, div.Width];

            if (ep.Type == EndpointType.BUS)
            {
                Data[div.Start] = dat;
                PendingData[div.Start] = dat;

                return;
            }

            if(ep.Type == EndpointType.REGISTER)
            {
                //PendingData[div.Start] = dat;
                PendingData.Write(dat, div.Start);

                return;
            }

            throw new FlowException($"Endpoint {ep.Name} has incorrect type {ep.Type}");
        }

        public override void Commit()
        {
            base.Commit();

            Data.Write(PendingData, 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator
{
    public class Architecture
    {
        protected List<ArchBlock> blocks = new();
        public ReadOnlyCollection<ArchBlock> Blocks => blocks.AsReadOnly();

        public IEnumerable<Register> Registers =>
            blocks.Where(x => x.GetType().IsAssignableTo(typeof(Register))).Select(x => (Register)x);

        protected List<Signal> signals = new();
        public ReadOnlyCollection<Signal> Signals => signals.AsReadOnly();

        protected List<Endpoint> endpoints = new();
        public ReadOnlyCollection<Endpoint> Endpoints => endpoints.AsReadOnly();

        public InstructionRegister InstructionRegister
        {
            get => Blocks
                .Where(x => x.GetType().IsAssignableTo(typeof(InstructionRegister)))
                .Select(x => (InstructionRegister)x)
                .First();
        }

        public int AddressWidth { get; } = 8;
        protected ConstProviderBlock constProvider = new();

        public Architecture()
        {
            AddBlock(constProvider);
        }

        public void AddBlock(ArchBlock b)
        {
            blocks.Add(b);
        }

        public void Clean()
        {
            signals.Clear();
            endpoints.Clear();

            foreach (var b in blocks)
            {
                b.Clean();
            }
        }

        public void CreateLive()
        {
            Clean();

            foreach(var b in blocks)
            {
                b.CreateLive(this);

                if (b.GetLive() is LiveBlock l)
                {
                    signals.AddRange(l.Signals);
                    endpoints.AddRange(l.Endpoints);
                }
            }
        }

        public Endpoint GetEndpoint(string name)
        {
            if (name.StartsWith("__const_"))
                return new Endpoint(name, constProvider);

            var ep = endpoints.Find(x => x.Name == name);

            if(ep == null)
                throw new FlowException($"Could not find memory endpoint with name {name}");

            return ep;
        }

        public Signal GetSignal(string name)
        {
            var sg = signals.Find(x => x.Name == name);

            if (sg == null)
                throw new FlowException($"Could not find signal with name {name}");

            return sg;
        }

        public void ExecSingleSignal(Signal sg)
        {
            var lb = sg.Owner.GetLive();

            lb.ExecuteSignal(this, sg);
        }

        public void ExecSingleSignal(string name)
        {
            ExecSingleSignal(GetSignal(name));
        }

        public void CleanDirty()
        {
            foreach (var b in Blocks)
                if (b.GetLive() is LiveBlock lb)
                    lb.SetNotDirty();
        }
        
        public bool IsAnyDirty()
        {
            return Blocks.Where(b => b.GetLive() != null).Where(b => b.GetLive().Dirty).Any();
        }

        public void ExecSignals(IEnumerable<Signal> sgs)
        {
            // TODO: Make signal order not matter
            int iterations = 0;

            do
            {
                CleanDirty();

                foreach (var s in sgs)
                    ExecSingleSignal(s);

                //if (!IsAnyDirty())
                    return;

                iterations++;
            }
            while (iterations < 50);

            throw new FlowException("Exceeded maximum amount of signal execution: probably two signals are poining to each other");
        }
    }
}

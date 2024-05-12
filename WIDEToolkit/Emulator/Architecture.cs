﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WIDEToolkit.Data.Exceptions;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.MemBlock;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator
{
    public class Architecture
    {
        public bool IsLive { get; protected set; } = false;

        protected List<ArchBlock> blocks = new();
        public ReadOnlyCollection<ArchBlock> Blocks => blocks.AsReadOnly();

        protected List<Signal> signals = new();
        public ReadOnlyCollection<Signal> Signals => signals.AsReadOnly();

        protected List<Endpoint> endpoints = new();
        public ReadOnlyCollection<Endpoint> Endpoints => endpoints.AsReadOnly();

        public string InstructionEndpoint = "";

        public int AddressWidth { get; } = 8;

        protected List<Signal> commitedSignals = new();
        protected List<Signal> currentSignals = new();
        public ReadOnlyCollection<Signal> CommitedSignals => commitedSignals.AsReadOnly();

        public MemBlock MemoryBlock => memBlock;
        protected MemBlock memBlock { get; } = new();
        protected ConstProviderBlock constProvider { get; } = new();
        protected DebugBlock debug { get; } = new();

        public Architecture()
        {
            AddBlock(constProvider);
            AddBlock(debug);
            AddBlock(memBlock);
        }

        public void AddBlock(ArchBlock b)
        {
            blocks.Add(b);
        }

        public void Clean()
        {
            IsLive = false;
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

            if(memBlock.Live is LiveMemBlock lmb)
                AttachMemoryDivisions(lmb.Divisions, true);

            IsLive = true;
        }

        protected void AttachMemoryDivisions(List<Memory> mems, bool throwOnNotFound = true)
        {
            var map = new Dictionary<string, Memory>();

            foreach (var m in mems)
                if(m.DivisionName is not null)
                    map[m.DivisionName] = m;

            foreach(var b in blocks)
            {
                if(b.GetLive() is IMemoryAttachable ima)
                {
                    var divName = ima.GetDivisionName();

                    if(!map.ContainsKey(divName))
                    {
                        if(throwOnNotFound)
                            throw new FlowException($"Could not attach memory {divName} to {b}, because it was not found.");

                        continue;
                    }

                    ima.AttachMemory(map[divName]);
                }
            }
        }

        public Endpoint GetEndpoint(string name)
        {
            if (name.StartsWith("__const_"))
                return new Endpoint(name, EndpointType.DISJOINTED_RO, constProvider);

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
            currentSignals.Add(sg);

            var lb = sg.Owner.GetLive();

            lb.ExecuteSignal(this, sg);
        }

        public void ExecSingleSignal(string name)
        {
            ExecSingleSignal(GetSignal(name));
        }

        public void ExecSignals(Signal[] signals)
        {
            foreach(var s in signals)
            {
                ExecSingleSignal(s);
            }
        }

        public void Commit()
        {
            foreach(var e in Blocks)
            {
                e.GetLive().Commit();
            }

            commitedSignals = currentSignals;
            currentSignals = new();
        }
    }
}

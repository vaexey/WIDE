using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.MemHandler;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Examples.W;

namespace WIDE.Controller
{
    public class EmulatorContainer : IDisposable
    {
        public Thread Thread { get; }

        public Architecture Arch { get; set; }
        public Emulator Emu { get; set; }

        private List<Action> invokeQueue = new();
        private object Lock = new object();

        private bool _disposed = false;
        private bool _thread = false;

        public EmulatorContainer(Architecture? arch = null)
        {
            //Arch = arch ?? new Architecture();
            //Emu = new Emulator(Arch, new());

            Arch = new WArchitecture();
            Arch.CreateLive();
            Emu = new Emulator(Arch, new WRawInstructionSet(Arch));

            var MH = (MemHandler)Arch.Blocks.Where(b => b.GetType() == typeof(MemHandler)).First();

            MH.Live.Memory = new SingleMemory(256);

            MH.Live.Memory.Write(0, WORD.FromUInt64(34, 8));
            MH.Live.Memory.Write(1, WORD.FromUInt64(160, 8));
            MH.Live.Memory.Write(2, WORD.FromUInt64(1, 8));

            Thread = new(EmulatorThreadSub);
        }

        public void Start()
        {
            Thread.Start();
        }

        public void EmulatorThreadSub()
        {
            _thread = true;

            while(!_disposed)
            {
                lock(Lock)
                {
                    while (invokeQueue.Count != 0)
                    {
                        var action = invokeQueue.First();
                        invokeQueue.RemoveAt(0);

                        action();
                    }

                    Emu.Loop();
                }

                if(Emu.Paused)
                    Thread.Sleep(1);
            }

            _thread = false;
        }

        public void InvokeUnpause()
        {
            lock(Lock)
            {
                Emu.Unpause();
            }
        }

        public void InvokePause()
        {
            lock (Lock)
            {
                Emu.Pause();
            }
        }

        public void InvokeCycle()
        {
            lock (Lock)
            {
                invokeQueue.Add(() =>
                {
                    Emu.Cycle();
                });
            }
        }
        public void InvokeInstruction()
        {
            lock (Lock)
            {
                invokeQueue.Add(() =>
                {
                    Emu.Instruction();
                });
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            while (_thread)
            { };
        }
    }
}

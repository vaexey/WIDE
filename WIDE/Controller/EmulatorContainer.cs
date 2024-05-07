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

        public Architecture Arch => Emu.Arch;

        public Emulator Emu { get; set; }

        public double? LoopSleep { get; set; } = null;

        private List<Action> invokeQueue = new();
        private object Lock = new object();

        private bool _disposed = false;
        private bool _thread = false;

        public EmulatorContainer(Architecture? arch = null)
        {
            //Arch = arch ?? new Architecture();
            //Emu = new Emulator(Arch, new());

            arch = new WArchitecture();
            arch.CreateLive();
            var riset = new WRawInstructionSet(arch);
            Emu = new Emulator(arch, riset);

            //var mem = new SingleMemory(256);
            //mem.DivisionName = "PaO";

            //Memory.Add(mem);
            //arch.AttachMemoryDivisions(Memory);

            //var MH = (MemHandler)Arch.Blocks.Where(b => b.GetType() == typeof(MemHandler)).First();

            //MH.Live.Memory = Memory;

            var mem = arch.MemoryBlock.Live.Get("PaO");
            //mem.Write(0, WORD.FromUInt64(35, 8));
            //mem.Write(1, WORD.FromUInt64(132,8));
            //mem.Write(2, WORD.FromUInt64(160, 8));
            //mem.Write(3, WORD.FromUInt64(1, 8));
            mem.Write(0, riset.Build("POB", 10));
            mem.Write(1, riset.Build("DOD", 11));
            mem.Write(2, riset.Build("LAD", 10));
            mem.Write(3, riset.Build("SOB", 0));

            mem.Write(11, WORD.FromUInt64(1));

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

                //if(Emu.Paused || LoopSleep is not null)
                //    Thread.Sleep(LoopSleep ?? 1);
                if (Emu.Paused)
                    Thread.Sleep(1);
                else
                {
                    if(LoopSleep is double ms)
                    {
                        if (ms > 100)
                            Thread.Sleep((int)ms);
                        else
                        {
                            var tim = DateTime.Now;
                            while (DateTime.Now.Subtract(tim).TotalMilliseconds < ms)
                            { }
                        }
                    }
                }
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
                Emu.Pause(EmulatorPauseReason.USER);
            }
        }

        public void Invoke(Action action)
        {
            lock(Lock)
            {
                invokeQueue.Add(action);
            }
        }

        public void WaitUntilInvokeCompleted(int? sleep = null)
        {
            while(true)
            {
                lock(Lock)
                {
                    if (invokeQueue.Count == 0)
                        break;
                }

                if(sleep is int ms)
                    Thread.Sleep(ms);
            }
        }

        public void InvokeCycle()
        {
            lock (Lock)
            {
                Invoke(() =>
                {
                    Emu.Cycle();
                });
            }
        }
        public void InvokeInstruction()
        {
            lock (Lock)
            {
                Invoke(() =>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.Examples.W;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Assembly;
using WIDEToolkit.Emulator.Blocks.ALU;
using WIDEToolkit.Emulator.Blocks.MemHandler;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Examples.W;

namespace WIDECLI.simpleW
{
    internal class SimpleW
    {
        private Emulator Emu;
        private RawInstructionSet ris;
        private WArchitecture W = new();
        private MemHandler MH;


        public void Start()
        {
            W.CreateLive();
            ris = new WRawInstructionSet(W);

            Emu = new(W, ris);

            MH = (MemHandler)W.Blocks.Where(b => b.GetType() == typeof(MemHandler)).First();

            MH.Live.Memory = new SingleMemory(256);

            MH.Live.Memory.Write(0, WORD.FromUInt64(36, 8));
            MH.Live.Memory.Write(1, WORD.FromUInt64(37, 8));
            MH.Live.Memory.Write(2, WORD.FromUInt64(38, 8));

            MH.Live.Memory.Write(4, WORD.FromUInt64(1, 8));
            MH.Live.Memory.Write(5, WORD.FromUInt64(2, 8));
            MH.Live.Memory.Write(6, WORD.FromUInt64(4, 8));

            while (true)
            {
                Draw();

                var line = Console.ReadLine();
                Console.Clear();


                if (line.StartsWith(">"))
                {
                    var sigs = line.Substring(1).Split(" ");
                    foreach (var sig in sigs)
                    {
                        W.ExecSingleSignal(sig);
                    }
                }
                else if(line.StartsWith("$"))
                {
                    var sp = line.Substring(1).Split(" ");

                    var addr = int.Parse(sp[0]);
                    var data = int.Parse(sp[1]);

                    MH.Live.Memory.Write(addr, WORD.FromUInt64((ulong)data, 8));
                }
                else if(line.StartsWith("#"))
                {
                    var sp = line.Substring(1).Split(" ");

                    var addr = W.GetEndpoint(sp[0]);
                    var data = int.Parse(sp[1]);

                    addr.WriteEndpoint(WORD.FromUInt64((ulong)data, 8));
                }
                else if(line.StartsWith("."))
                {
                    Emu.Cycle();
                }
                else
                {
                    Console.WriteLine(">signal $addr #ep .cycle");
                }

                W.Commit();
            }
        }

        private void Draw()
        {
            Console.WriteLine("Cycle: {0}, Instr opcode: {1}, Instr: {2}", 
                Emu.CycleIndex, 
                Emu.CurrentInstruction?.OpCode.ToString() ?? "-",
                Emu.CurrentInstruction?.Name ?? "-");

            for(int i = 0; i < W.Blocks.Count; i++)
            {
                var b = W.Blocks[i];

                if(b is Register reg)
                {
                    Console.WriteLine("#{0}: Reg {1}", i, reg.BaseName);

                    foreach (var div in reg.Live.Divisions)
                    {
                        Console.WriteLine("     {0} D{1}:{2} <- {3}", div.Name, div.Start, div.End, div.Endpoint.ReadEndpoint(div.Width).ToUInt64());
                    }
                }
                else if(b is ALU alu)
                {
                    Console.WriteLine("#{0}: ALU {1}", i, alu.BaseName);
                }
                else if(b is MemHandler mem)
                {
                    Console.WriteLine("#{0}: Memory {1}", i, mem.BaseName);

                    for(int a = 0; a < 16; a++)
                    {
                        Console.WriteLine("     {0} <- {1}", a.ToString().PadLeft(3, '0'), mem.Live.Memory.Read(a).ToUInt64());
                    }

                    Console.WriteLine("     ...");
                }
                else
                {
                    Console.WriteLine("#{0}: {1}", i, b.GetType().Name);
                }

                Console.WriteLine("          >> {0} <<", string.Join(' ', b.GetLive().Signals.Select(x => x.Name).ToArray()));
            }
        }
    }
}

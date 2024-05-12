using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.ALU;
using WIDEToolkit.Emulator.Blocks.MemHandler;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Flow;
using static WIDEToolkit.Emulator.Blocks.Register.Register;

namespace WIDEToolkit.Examples.W
{
    public class WArchitecture : Architecture
    {
        public WArchitecture()
        {
            InstructionEndpoint = "IKOD";

            // BUS S
            AddBlock(new Register()
            {
                BaseName = "magS",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.BUS,
                    }
                },
                Meta = new("magS", 50, 400, 400, 35)
            }) ;

            // BUS A
            AddBlock(new Register()
            {
                BaseName = "magA",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.BUS,
                    }
                },
                Meta = new("magA", 50, 50, 400, 35)
            });

            // TODO INSTR REGISTER
            // INSTRUCTION REGISTER I
            AddBlock(new Register()
            {
                BaseName = "I",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameFormat = "I",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.REGISTER,
                    }.WithSignal("wei", "magS", RegisterSignalMode.LOAD),
                    new RegisterDivisionBlueprint()
                    {
                        NameFormat = "IAD",
                        Start = 0,
                        End = 5,
                        EndpointType = EndpointType.DISJOINTED_RO,
                    }.WithSignal("wyad", "magA", RegisterSignalMode.STORE),
                    new RegisterDivisionBlueprint()
                    {
                        NameFormat = "IKOD",
                        Start = 5,
                        End = 8,
                        EndpointType = EndpointType.DISJOINTED_RO,
                    }
                },
                Meta = new("Rejestr I", 50, 300, 100, 100)
            });

            // COUNTER REGISTER L
            AddBlock(new Register()
            {
                BaseName = "L",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        //SignalAdd = new("__const_1", "il"),
                        EndpointType = EndpointType.REGISTER,
                    }.WithSignal("wel", "magA", RegisterSignalMode.LOAD)
                    .WithSignal("wyl", "magA", RegisterSignalMode.STORE)
                },
                Meta = new("Rejestr L", 50, 100, 100, 100)
            });

            // ACCUMULATOR REGISTER AK
            AddBlock(new Register()
            {
                BaseName = "Ak",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.REGISTER,
                    }.WithSignal("wyak", "magS", RegisterSignalMode.STORE)
                },
                Meta = new("Rejestr AK", 200, 100, 100, 100)
            });

            // ALU JAL
            AddBlock(new Register()
            {
                BaseName = "JAL_WE",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.BUS,
                    }.WithSignal("weja", "magS", RegisterSignalMode.LOAD)
                }
            });

            AddBlock(new Register()
            {
                BaseName = "JAL_WY",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.BUS,
                    }.WithSignal("weak", "Ak", RegisterSignalMode.STORE)
                }
            });

            AddBlock(new ALU()
            {
                BaseName = "JAL",
                Signals = new()
                {
                    new(AnonALUOperation.Sum(8))
                    {
                        NameFormat = "dod",
                        SourceEndpointFormats = new() { "JAL_WE", "Ak" },
                        DestEndpointFormats = new() { "JAL_WY" }
                    },
                    new(AnonALUOperation.From((ins, outs) => outs[0].WriteEndpoint(
                            ins[1].ReadEndpoint(8) - ins[0].ReadEndpoint(8)
                        )))
                    {
                        NameFormat = "ode",
                        SourceEndpointFormats = new() { "JAL_WE", "Ak" },
                        DestEndpointFormats = new() { "JAL_WY" }
                    },
                    new(AnonALUOperation.From((ins, outs) => outs[0].WriteEndpoint(ins[0].ReadEndpoint(8))))
                    {
                        NameFormat = "przep",
                        SourceEndpointFormats = new() { "JAL_WE", "Ak" },
                        DestEndpointFormats = new() { "JAL_WY" }
                    },
                    new(AnonALUOperation.Sum(8))
                    {
                        NameFormat = "il",
                        SourceEndpointFormats = new() { "L", "__const_1" },
                        DestEndpointFormats = new() { "L" }
                    }
                },
                Meta = new("JAL", 200, 200, 100, 100)
            });

            // MEMORY PAO
            AddBlock(new Register()
            {
                BaseName = "S",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.REGISTER,
                    }.WithSignal("wys", "magS", RegisterSignalMode.STORE)
                    .WithSignal("wes", "magS", RegisterSignalMode.LOAD),
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}_immediate",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.BUS,
                    }
                },
                Meta = new("Rejestr S", 350, 300, 100, 100)
            });

            AddBlock(new Register()
            {
                BaseName = "A",
                AdjustRegisterToAddressSize = true,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        EndpointType = EndpointType.REGISTER,
                    }.WithSignal("wea", "magA", RegisterSignalMode.LOAD)
                },
                Meta = new("Rejestr A", 350, 100, 100, 100)
            });

            AddBlock(new MemHandler()
            {
                BaseName = "PaO",
                AddresEndpointFormat = "A",
                WriteEndpointFormat = "S",
                ReadEndpointFormat = "S_immediate",
                
                WriteSignalFormat = "pisz",
                ReadSignalFormat = "czyt",
                AddressWidth = 8 - 3,

                Meta = new("PaO", 350, 200, 100, 100)
            });

            MemoryBlock.MemoryDivisions.Add(
                new()
                {
                    DivisionName = "PaO",
                    Size = 256,
                    WordSize = 8
                });
            //MemoryBlock.MemoryDivisions.Add(
            //    new()
            //    {
            //        DivisionName = "PaO",
            //        Size = 65536,
            //        WordSize = 8
            //    });
        }
    }
}

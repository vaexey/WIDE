using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.ALU;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Flow;
using static WIDEToolkit.Emulator.Blocks.Register.Register;

namespace WIDE.Examples.W
{
    public class WArchitecture : Architecture
    {
        public WArchitecture()
        {
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
                Meta = new()
                {
                    Y = 100,
                    Title = "magS"
                }
            });

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
                }
            });

            // TODO INSTR REGISTER
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
                        Start = 3,
                        End = 8,
                        EndpointType = EndpointType.DISJOINTED_RO,
                    }.WithSignal("wyad", "magA", RegisterSignalMode.STORE),
                    new RegisterDivisionBlueprint()
                    {
                        NameFormat = "IKOD",
                        Start = 0,
                        End = 3,
                        EndpointType = EndpointType.DISJOINTED_RO,
                    }
                }
            });

            // TODO INCREMENT
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
                }
            });

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
                }
            });

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
                    }
                }
            });

            //AddBlock(new ALUBlock()
            //{
            //    BaseName = "JAL",
            //    Operations = new ALUOperationDesc[]
            //    {
            //        new()
            //        {
            //            Operation = ALUOperation.ADD,
            //            SignalFormat = "dod",
            //            EndpointFormat1 = "Ak",
            //            EndpointFormat2 = "magS",
            //            EndpointFormat3 = "Ak",
            //            AdjustToAddressSize = true,
            //        },
            //        new()
            //        {
            //            Operation = ALUOperation.PASS,
            //            SignalFormat = "przep",
            //            EndpointFormat1 = "Ak",
            //            EndpointFormat2 = "magS",
            //            EndpointFormat3 = "Ak",
            //            AdjustToAddressSize = true,
            //        }
            //    }
            //});

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks;
using static WIDEToolkit.Emulator.Blocks.ALUBlock;
using static WIDEToolkit.Emulator.Blocks.Register;

namespace WIDE.Examples.W
{
    public class WArchitecture : Architecture
    {
        public WArchitecture()
        {
            AddBlock(new BusRegister()
            {
                BaseName = "magS",
                AdjustRegisterToAddressSize = true,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8
                    }
                },
                Meta = new()
                {
                    Y = 100,
                    Title = "magS"
                }
            });

            AddBlock(new BusRegister()
            {
                BaseName = "magA",
                AdjustRegisterToAddressSize = true,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8
                    }
                }
            });

            AddBlock(new InstructionRegister()
            {
                BaseName = "I",
                AdjustRegisterToAddressSize = true,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameFormat = "I",
                        Start = 0,
                        End = 8,
                        SignalIn = new("magS", "wei")
                    },
                    new RegDivision()
                    {
                        NameFormat = "IAD",
                        Start = 3,
                        End = 8,
                        SignalOut = new("magA", "wyad")
                    },
                    new RegDivision()
                    {
                        NameFormat = "IKOD",
                        Start = 0,
                        End = 3
                    }
                }
            });

            AddBlock(new Register()
            {
                BaseName = "L",
                AdjustRegisterToAddressSize = true,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        SignalIn = new("magA", "wel"),
                        SignalOut = new("magA", "wyl"),
                        SignalAdd = new("__const_1", "il"),
                    }
                }
            });

            AddBlock(new Register()
            {
                BaseName = "Ak",
                AdjustRegisterToAddressSize = true,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{0}",
                        Start = 0,
                        End = 8,
                        SignalOut = new("magS", "wyak"),
                    }
                }
            });

            AddBlock(new ALUBlock()
            {
                BaseName = "JAL",
                Operations = new ALUOperationDesc[]
                {
                    new()
                    {
                        Operation = ALUOperation.ADD,
                        SignalFormat = "dod",
                        EndpointFormat1 = "Ak",
                        EndpointFormat2 = "magS",
                        EndpointFormat3 = "Ak",
                        AdjustToAddressSize = true,
                    },
                    new()
                    {
                        Operation = ALUOperation.PASS,
                        SignalFormat = "przep",
                        EndpointFormat1 = "Ak",
                        EndpointFormat2 = "magS",
                        EndpointFormat3 = "Ak",
                        AdjustToAddressSize = true,
                    }
                }
            });

        }
    }
}

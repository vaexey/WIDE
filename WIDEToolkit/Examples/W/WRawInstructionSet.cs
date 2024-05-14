﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Flow;
using WIDEToolkit.Emulator.Assembly;

namespace WIDEToolkit.Examples.W
{
    public class WRawInstructionSet : RawInstructionSet
    {
        public WRawInstructionSet(Architecture arch, int opcodeWidth = 3)
        {
            Instructions = new RawInstruction[]
            {

                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(0ul), "STP", new() {
                        "czyt wys wei il",
                        "__stop"
                    }),
                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(1ul), "DOD", new() {
                        "czyt wys wei il",
                        "wyad wea",
                        "czyt wys weja dod weak wyl wea",
                    }),
                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(2ul), "ODE", new() {
                        "czyt wys wei il",
                        "wyad wea",
                        "czyt wys weja ode weak wyl wea",
                    }),
                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(3ul), "POB", new() {
                        "czyt wys wei il",
                        "wyad wea",
                        "czyt wys weja przep weak wyl wea",
                    }),
                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(4ul), "LAD", new() {
                        "czyt wys wei il",
                        "wyad wea wyak wes",
                        "pisz wyl wea",
                    }),
                RawInstruction.FromStrings(
                    arch, WORD.FromUInt64(5ul), "SOB", new() {
                        "czyt wys wei il",
                        "wyad wea wel"
                    }),
                new RawInstruction(
                    WORD.FromUInt64(6ul),
                    "SOM",
                    new Signal[][][]
                    {
                        new Signal[][]
                        {
                            "czyt wys wei il".Split(" ").Select(s => arch.GetSignal(s)).ToArray(),
                            "czyt wys wei il".Split(" ").Select(s => arch.GetSignal(s)).ToArray(),
                        },
                        new Signal[][]
                        {
                            "wyl wea".Split(" ").Select(s => arch.GetSignal(s)).ToArray(),
                            "wyad wea wel".Split(" ").Select(s => arch.GetSignal(s)).ToArray(),
                        }
                    },
                    new ForkEvaluator(
                            new ForkComponent[]
                            {
                                new ForkComponent("Z", 1)
                            }
                        )
                )
            };
        }

        public WORD Build(string instr, int arg)
        {
            var opcode = Instructions.Where(i => i.Name == instr).First().OpCode.Slice(0, 3);

            var bin = WORD.Zero(8);

            var warg = WORD.FromUInt64((ulong)arg, 5);

            bin.Write(opcode, 5);
            bin.Write(warg, 0);

            return bin;
        }
    }
}

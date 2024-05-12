﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Assembly
{
    public class RawInstruction
    {
        public WORD OpCode { get; }
        public string Name { get; }

        /// <summary>
        /// Signal[t][f][i]
        ///  t - instruction time index
        ///  f - fork value
        ///  i - signal in subset index
        /// </summary>
        public Signal[][][] Cycles { get; }

        public ForkEvaluator Forks { get; }

        public RawInstruction(WORD opcode, string name, Signal[][][] cycles, ForkEvaluator? forks = null)
        {
            OpCode = opcode;
            Name = name;
            Cycles = cycles;
            Forks = forks ?? new ForkEvaluator();
        }

        public static RawInstruction FromStrings(Architecture arch, WORD opcode, string name, List<string> cycles)
        {
            return new RawInstruction(
                  opcode,
                  name,
                  cycles.Select(x => new Signal[][] { x.Split(" ").Select(s => arch.GetSignal(s)).ToArray() }).ToArray()
              );
        }
    }
}

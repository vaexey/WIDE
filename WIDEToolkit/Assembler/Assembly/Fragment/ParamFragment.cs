using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler.Assembly.Fragment
{
    public class ParamFragment : AsmInstructionFragment
    {
        public int Index { get; }
        public int Width { get; }

        public ParamFragment(int index, int width)
        {
            Index = index;
            Width = width;
        }

        private IAsmSymbol GetSymbol(AsmImplementedInstruction instr)
        {
            if (Index >= instr.Params.Count)
                throw new AsmInstructionException($"Param index ({Index}) out of range (0-{instr.Params.Count - 1}");

            return instr.Params[Index];
        }

        public override WORD Build(AsmImplementedInstruction instr)
        {
            var param = GetSymbol(instr);

            if (param is IReadableAsmSymbol ras)
            {
                return ras.Read().Slice(0, Width);
            }

            throw new AsmInstructionException($"Param ({param}) at index {Index} is not readable");
        }

        public override int CalculateWidth(AsmImplementedInstruction instr)
        {
            return Width;
            //var param = GetSymbol(instr);

            //if(param is IReadableAsmSymbol ras)
            //{
            //    return ras.Read().Width;
            //} 
            
            //if(param is ITranslatableAsmSymbol tas)
            //{
            //    return tas.CalculateWidth(instr);
            //}

            //throw new AsmInstructionException($"Param ({param}) at index {Index} is not readable nor translatable");
        }
    }
}

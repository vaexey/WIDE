using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly
{
    public class AsmImplementedInstruction
    {
        public AsmInstructionImplementation Parent { get; set; }
        public List<IAsmSymbol> Params { get; set; } = new();

        public int Offset { get; set; } = 0;
        public int Width { get; set; } = 0;

        public AsmImplementedInstruction(AsmInstructionImplementation parent)
        {
            Parent = parent;
        }

        public WORD Build()
        {
            WORD result = WORD.Zero(Width);

            int index = 0;

            foreach(var frag in Parent.Fragments)
            {
                var fWord = frag.Build(this);

                result.Write(fWord, index);

                index += fWord.Width;
            }

            return result;
        }
    }
}

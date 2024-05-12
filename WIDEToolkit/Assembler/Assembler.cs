using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Assembler.Parsing;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Data.Exceptions;

namespace WIDEToolkit.Assembler
{
    public class Assembler
    {
        public AsmInstructionSet Set { get; set; } = new();

        public int AddressWidth { get; set; } = 5;

        public WORD Assemble(string listing)
        {
            return Assemble(AsmParser.Parse(listing));
        }

        public WORD Assemble(List<List<IAsmSymbol>> listing)
        {
            var impls = GenerateImplementations(listing);
            var code = BuildImplementations(impls);

            return code;
        }

        public List<AsmImplementedInstruction> GenerateImplementations(List<List<IAsmSymbol>> listing)
        {
            List<AsmImplementedInstruction> impls = new();
            Dictionary<string, WORD> labelTable = new();

            int index = 0;

            // 1st pass
            foreach(var symbolsIter in listing)
            {
                var symbols = symbolsIter;

                string? newLabelName = null;

                if(symbols.Count > 0 &&
                    symbols[0] is NewLabelSymbol nls)
                {
                    symbols = symbolsIter.ToList();
                    symbols.RemoveAt(0);

                    newLabelName = nls.Name;
                }

                var impl = Set.ParseInstruction(symbols);

                if (impl is null)
                    throw new AsmInstructionException($"No instruction matches symbols");

                impl.Offset = index;

                if (newLabelName != null)
                    labelTable[newLabelName] = WORD.FromUInt64(unchecked((ulong)impl.Offset), AddressWidth);

                index += impl.Width;

                impls.Add(impl);
            }

            //2nd pass
            foreach(var impl in impls)
            {
                for(int i = 0; i < impl.Params.Count; i++)
                {
                    var param = impl.Params[i];

                    if(param is ITranslatableAsmSymbol tas)
                    {
                        impl.Params[i] = tas.Translate(labelTable);
                    }
                }
            }

            return impls;
        }

        public WORD BuildImplementations(List<AsmImplementedInstruction> impls)
        {
            var words = impls.Select(i => i.Build()).ToArray();

            var totalWidth = words.Select(w => w.Width).Sum();

            WORD code = WORD.Zero(totalWidth);

            int index = 0;
            foreach (var word in words)
            {
                code.Write(word, index);

                index += word.Width;
            }

            return code;
        }

        //public List<string> Reconstruct(List<AsmImplementedInstruction> impls)
        //{
        //    return impls.Select(i => i.)
        //}
    }
}

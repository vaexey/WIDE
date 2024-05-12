using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler;
using WIDEToolkit.Assembler.Parsing;
using WIDEToolkit.Examples.W;

namespace WIDECLI
{
    internal class SimpleAssembler
    {
        public void Start()
        {
            var asm = new Assembler();
            asm.Set = new WAsmInstructionSet();

            var text = File.ReadAllText("./text.lst");

            var listing = AsmParser.Parse(text);

            var impls = asm.GenerateImplementations(listing);
            var code = asm.BuildImplementations(impls);

            var bin = code.ToBytes();

            File.WriteAllBytes("./text.bin", bin);
        }
    }
}

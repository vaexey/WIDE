using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Data.Binary;

namespace WIDEToolkit.Assembler.Assembly
{
    public class AsmInstruction
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public WORD Opcode { get; set; }

        public List<AsmInstructionImplementation> Implementations { get; set; } = new();

        public AsmForkDefinition Fork { get; set; } = new();
        public List<AsmSignalSet> Signals { get; set; } = new();

        public AsmInstruction(string name, WORD opcode)
        {
            Name = name;
            Opcode = opcode;
        }

        public AsmImplementedInstruction? TryMatch(List<IAsmSymbol> symbols)
        {
            foreach(var impl in Implementations)
            {
                var result = impl.TryMatch(symbols);

                if(result is not null)
                    return result;
            }

            return null;
        }

        public static AsmInstruction Create(string name, WORD opcode) 
        {
            return new AsmInstruction(name, opcode);
        }

        public AsmInstructionImplementation WithImplementation()
        {
            var aii = new AsmInstructionImplementation(this);

            Implementations.Add(aii);

            return aii;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Assembler.Parsing;

namespace ToolkitUnitTests.Assembler.Parsing
{
    [TestClass]
    public class AsmParserLineTest
    {
        [TestMethod]
        [DataRow("mov RAX, [a + 1]", typeof(LabelSymbol), typeof(LabelSymbol), typeof(AddressSymbol))]
        [DataRow("entry: mov RAX, [a + 1]", typeof(NewLabelSymbol), typeof(LabelSymbol), typeof(LabelSymbol), typeof(AddressSymbol))]
        public void ParsedTypes(string line, params Type[] types)
        {
            var symbols = AsmParser.ParseLine(line);
            var symbolTypes = symbols.Select(s => s.GetType()).ToArray();

            Assert.IsTrue(types.SequenceEqual(symbolTypes));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Assembly.Symbol;
using WIDEToolkit.Assembler.Parsing;
using WIDEToolkit.Data.Exceptions;

namespace ToolkitUnitTests.Assembler.Parsing
{
    [TestClass]
    public class AsmParserSymbolTest
    {
        private T AssertSymbol<T>(IAsmSymbol? symbol) where T : IAsmSymbol
        {
            Assert.IsNotNull(symbol);
            Assert.IsInstanceOfType(symbol, typeof(T));

            return (T)symbol;
        }

        [TestMethod]
        [DataRow("label")]
        [DataRow("TestLAbel")]
        [DataRow("LongLabel123_aaa23")]
        public void ParseLabelSymbol(string symbol)
        {
            var parsed = AsmParser.ParseSymbol(symbol);

            var ls = AssertSymbol<LabelSymbol>(parsed);

            Assert.AreEqual(symbol, ls.Name);
        }

        [TestMethod]
        [DataRow("label:")]
        [DataRow("TestLAbel:")]
        [DataRow("LongLabel123_aaa23:")]
        public void ParseNewLabelSymbol(string symbol)
        {
            var parsed = AsmParser.ParseSymbol(symbol);

            var ls = AssertSymbol<NewLabelSymbol>(parsed);

            Assert.AreEqual(symbol.Substring(0, symbol.Length - 1), ls.Name);
        }

        [TestMethod]
        [DataRow("1233", 1233ul)]
        [DataRow("0xff", 255ul)]
        [DataRow("0FFH", 255ul)]
        public void ParseImmediateSymbol(string symbol, ulong value)
        {
            var parsed = AsmParser.ParseSymbol(symbol);

            var ims = AssertSymbol<ImmediateSymbol>(parsed);

            Assert.AreEqual(value, ims.Value);
        }

        [TestMethod]
        [DataRow("1233 + 1233", 1)]
        [DataRow("0xff - 0xFF", -1)]
        [DataRow("0FFH + 0ffh", 1)]
        public void ParseOffsetSymbol(string symbol, int oper)
        {
            var parsed = AsmParser.ParseSymbol(symbol);

            var os = AssertSymbol<OffsetSymbol>(parsed);

            Assert.AreEqual(os.Operator, oper);
        }

        [TestMethod]
        [DataRow("[1233]")]
        [DataRow("[0xff + 0xff]")]
        [DataRow("[[0FFH] + 0FFH]")]
        [DataRow("[[0FFH] + [label]]")]
        public void ParseAddressSymbol(string symbol)
        {
            var parsed = AsmParser.ParseSymbol(symbol);

            AssertSymbol<AddressSymbol>(parsed);
        }

        [TestMethod]
        [DataRow("Invalid Label:")]
        [DataRow("0ff")]
        [DataRow("[[address]")]
        [DataRow("invalid,label")]
        [DataRow("+12")]
        [DataRow("50+")]
        [DataRow("[+120]")]
        public void ParseInvalidSymbol(string symbol)
        {
            Assert.ThrowsException<AssemblerException>(() => AsmParser.ParseSymbol(symbol));
        }
    }
}

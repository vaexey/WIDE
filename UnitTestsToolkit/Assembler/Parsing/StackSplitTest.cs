using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Assembler.Parsing;

namespace ToolkitUnitTests.Assembler.Parsing
{
    [TestClass]
    public class StackSplitTest
    {
        [TestMethod]
        [DataRow("a b c d", "a_b_c_d", ' ', '_')]
        [DataRow("Abc de FGh", "Abc_de_FGh", ' ', '_')]
        [DataRow("Abc de  FGh", "Abc_de__FGh", ' ', '_')]
        [DataRow("abc", "abc", ' ', '_')]
        public void EmptySet(string input, string output, char delimeter, char join)
        {
            var split = input.StackSplit(delimeter, new());

            Assert.AreEqual(output, string.Join(join, split));
        }

        [TestMethod]
        [DataRow(
            "ABC DEF GHI", 
            "ABC*DEF*GHI")]
        [DataRow(
            "abc(def, ghi), ijklmn[opq + rstuv]", 
            "abc(def, ghi),*ijklmn[opq + rstuv]")]
        [DataRow(
            "abc(def ghi [ijk lmno pqr]) (rst uvw) zzz",
            "abc(def ghi [ijk lmno pqr])*(rst uvw)*zzz")]
        public void BracketSet(string input, string output)
        {
            var split = input.StackSplit(' ', 
                new()
                {
                    new('[', ']'),
                    new('(', ')')
                });

            Assert.AreEqual(output, string.Join('*', split));
        }
    }
}

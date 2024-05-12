using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Examples.W;
using a = WIDEToolkit.Emulator;

namespace ToolkitUnitTests.Emulator
{
    [TestClass]
    public class EmulatorTest
    {
        public a.Emulator CreateW()
        {
            var warch = new WArchitecture();
            warch.CreateLive();

            var iset = new WRawInstructionSet(warch);

            var emu = new a.Emulator(warch, iset);

            return emu;
        }

        [TestMethod]
        public void DOD()
        {
            var emu = CreateW();

            emu.Arch.GetEndpoint("Ak").WriteEndpoint(WORD.FromUInt64(25ul));
            emu.Arch.Commit();

            emu.Arch.GetEndpoint("magS").WriteEndpoint(WORD.FromUInt64(1ul));
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;
using static WIDEToolkit.Emulator.Blocks.Live.LiveRegister;

namespace UnitTestsToolkit.Emulator.Blocks.Live
{
    [TestClass]
    public class LiveRegisterTest
    {
        private LiveRegister Create()
        {
            return new LiveRegister(
                    null,
                    8,
                    Array.Empty<Signal>(),
                    Array.Empty<Endpoint>(),
                    new LiveRegisterDivision[]
                    {
                        new("a", 0, 5),
                        new("b", 5, 8)
                    }
                );
        }

        [TestMethod]
        public void ReadWrite1()
        {
            var x = Create();

            var src = WORD.FromUInt64(0xdeadbeef);

            x.WriteEndpoint(null, new Endpoint("a", null), src);
            x.WriteEndpoint(null, new Endpoint("b", null), src);

            var a = x.ReadEndpoint(null, new Endpoint("a", null), 64);
            var b = x.ReadEndpoint(null, new Endpoint("b", null), 32);

            Assert.AreEqual(64, a.Width);
            Assert.AreEqual(32, b.Width);

            Assert.AreEqual(0b1111, a.ToBytes()[0]);
            Assert.AreEqual(0b111, b.ToBytes()[0]);
        }

        [TestMethod]
        public void Increment()
        {
            var x = Create();
            var a = new Architecture();
            a.CreateLive();

            x.WriteEndpoint(null, new Endpoint("a", null), WORD.FromUInt64(2));

            x.ExecuteSignal(
                a,
                new RegisterSignal(
                    "inc",
                    "__const_10",
                    null,
                    0,
                    RegisterSignal.RegisterSignalMode.SUM
                )
            );

            Assert.AreEqual(12, x.Data.ToBytes()[0]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Flow;
using static WIDEToolkit.Emulator.Blocks.Register.LiveRegister;

namespace ToolkitUnitTests.Emulator.Blocks
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
                        new("a", 0, 5, new Endpoint(null, EndpointType.BUS, null)),
                        new("b", 5, 8, new Endpoint(null, EndpointType.BUS, null))
                    }
                );
        }

        [TestMethod]
        public void ReadWrite1()
        {
            var x = Create();

            var src = WORD.FromUInt64(0xdeadbeef);

            x.WriteEndpoint(new Endpoint("a", EndpointType.BUS, null), src);
            x.WriteEndpoint(new Endpoint("b", EndpointType.BUS, null), src);

            var a = x.ReadEndpoint(new Endpoint("a", EndpointType.BUS, null), 64);
            var b = x.ReadEndpoint(new Endpoint("b", EndpointType.BUS, null), 32);

            Assert.AreEqual(64, a.Width);
            Assert.AreEqual(32, b.Width);

            Assert.AreEqual(0b1111, a.ToBytes()[0]);
            Assert.AreEqual(0b111, b.ToBytes()[0]);
        }

        //[TestMethod]
        //public void Increment()
        //{
        //    var x = Create();
        //    var a = new Architecture();
        //    a.CreateLive();

        //    x.WriteEndpoint(new Endpoint("a", EndpointType.BUS, null), WORD.FromUInt64(2));

        //    x.ExecuteSignal(
        //        a,
        //        new RegisterSignal(
        //            "inc",
        //            "__const_10",
        //            null,
        //            0,
        //            RegisterSignalMode.SUM
        //        )
        //    );

        //    Assert.AreEqual(12, x.Data.ToBytes()[0]);
        //}
    }
}

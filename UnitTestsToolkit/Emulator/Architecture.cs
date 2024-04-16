using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;
using static WIDEToolkit.Emulator.Blocks.Register;

namespace UnitTestsToolkit.Emulator
{
    [TestClass]
    public class ArchitectureTest
    {
        [TestMethod]
        public void BasicRegisterTransfer()
        {
            Architecture a = new Architecture();

            a.AddBlock(new Register()
            {
                BaseName = "AX",
                Width = 64,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.)X$"),
                        NameFormat = "R{1}X",
                        Start = 0,
                        End = 64,
                        SignalIn = new("BUS", "in_R{1}X"),
                        SignalOut = new("BUS", "out_R{1}X"),
                    },
                    new RegDivision()
                    {
                        NameRegex = new("^(.)X$"),
                        NameFormat = "E{1}X",
                        Start = 0,
                        End = 32,
                        SignalIn = new("BUS", "in_E{1}X"),
                        SignalOut = new("BUS", "out_E{1}X"),
                    }
                }
            });

            a.AddBlock(new Register()
            {
                BaseName = "BUS",
                Width = 64,
                Divisions = new RegDivision[]
                {
                    new RegDivision()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{1}",
                        Start = 0,
                        End = 64,
                    }
                }
            });

            a.CreateLive();

            var rax = a.GetEndpoint("RAX");
            var eax = a.GetEndpoint("EAX");
            var bus = a.GetEndpoint("BUS");

            rax.WriteEndpoint(a, WORD.FromUInt64(0));
            bus.WriteEndpoint(a, WORD.FromUInt64(0));

            Assert.AreEqual(0ul, rax.ReadEndpoint(a, 64).ToUInt64());
            Assert.AreEqual(0ul, bus.ReadEndpoint(a, 64).ToUInt64());

            rax.WriteEndpoint(a, WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul));

            Assert.AreEqual(0xFFFFFFFFFFFFFFFFul, rax.ReadEndpoint(a, 64).ToUInt64());

            a.ExecSingleSignal("out_RAX");

            Assert.AreEqual(0xFFFFFFFFFFFFFFFFul, bus.ReadEndpoint(a, 64).ToUInt64());

            Assert.ThrowsException<FlowException>(() =>
            {
                a.ExecSignals(new Signal[]
                {
                    a.GetSignal("out_RAX"),
                    a.GetSignal("in_RAX")
                });
            });

            rax.WriteEndpoint(a, WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul));
            bus.WriteEndpoint(a, WORD.FromUInt64(0ul));

            a.ExecSignals(new Signal[]
            {
                a.GetSignal("in_EAX")
            });

            Assert.AreEqual(0xFFFFFFFF00000000ul, rax.ReadEndpoint(a, 64).ToUInt64());
        }

        [TestMethod]
        public void ConstProvider()
        {
            var a = new Architecture();

            a.CreateLive();

            var w = a.GetEndpoint("__const_513").ReadEndpoint(a, 34);

            Assert.AreEqual(513ul, w.ToUInt64());
        }
    }
}

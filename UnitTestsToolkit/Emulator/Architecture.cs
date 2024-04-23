using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.Examples.W;
using WIDEToolkit.Emulator;
using WIDEToolkit.Emulator.Blocks.Live;
using WIDEToolkit.Emulator.Blocks.Register;
using WIDEToolkit.Emulator.Data;
using WIDEToolkit.Emulator.Flow;
using WIDEToolkit.Examples.W;
using static WIDEToolkit.Emulator.Blocks.Register.Register;

namespace ToolkitUnitTests.Emulator
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
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.)X$"),
                        NameFormat = "R{1}X",
                        Start = 0,
                        End = 64,
                        EndpointType = EndpointType.REGISTER
                    }.WithSignal("in_R{1}X", "BUS", RegisterSignalMode.LOAD)
                    .WithSignal("out_R{1}X", "BUS", RegisterSignalMode.STORE),
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.)X$"),
                        NameFormat = "E{1}X",
                        Start = 0,
                        End = 32,
                        EndpointType = EndpointType.REGISTER
                    }.WithSignal("in_E{1}X", "BUS", RegisterSignalMode.LOAD)
                    .WithSignal("out_E{1}X", "BUS", RegisterSignalMode.STORE),
                }
            });

            a.AddBlock(new Register()
            {
                BaseName = "BUS",
                Width = 64,
                Divisions = new()
                {
                    new RegisterDivisionBlueprint()
                    {
                        NameRegex = new("^(.*)$"),
                        NameFormat = "{1}",
                        Start = 0,
                        End = 64,
                        EndpointType = EndpointType.BUS
                    }
                }
            });

            a.CreateLive();

            var rax = a.GetEndpoint("RAX");
            var eax = a.GetEndpoint("EAX");
            var bus = a.GetEndpoint("BUS");

            rax.WriteEndpoint(WORD.FromUInt64(0));
            bus.WriteEndpoint(WORD.FromUInt64(0));

            a.Commit();

            Assert.AreEqual(0ul, rax.ReadEndpoint(64).ToUInt64());

            Assert.AreEqual(0ul, bus.ReadEndpoint(64).ToUInt64());

            rax.WriteEndpoint(WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul));

            Assert.AreEqual(0ul, rax.ReadEndpoint(64).ToUInt64());
            a.Commit();
            Assert.AreEqual(0xFFFFFFFFFFFFFFFFul, rax.ReadEndpoint(64).ToUInt64());

            a.ExecSingleSignal("out_RAX");

            Assert.AreEqual(0xFFFFFFFFFFFFFFFFul, bus.ReadEndpoint(64).ToUInt64());

            //Assert.ThrowsException<FlowException>(() =>
            //{
            //    a.ExecSignals(new Signal[][]
            //    {
            //        new Signal[]
            //        {
            //            a.GetSignal("out_RAX"),
            //            a.GetSignal("in_RAX")
            //        }
            //    });
            //});

            rax.WriteEndpoint(WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul));
            bus.WriteEndpoint(WORD.FromUInt64(0ul));
            a.Commit();

            a.ExecSignals(new Signal[]
            {
                a.GetSignal("in_EAX")
            });

            a.Commit();

            Assert.AreEqual(0xFFFFFFFF00000000ul, rax.ReadEndpoint(64).ToUInt64());
        }

        [TestMethod]
        public void ConstProvider()
        {
            var a = new Architecture();

            a.CreateLive();

            var w = a.GetEndpoint("__const_513").ReadEndpoint(34);

            Assert.AreEqual(513ul, w.ToUInt64());
        }

        [TestMethod]
        public void DOD()
        {
            var a = new WArchitecture();
            a.CreateLive();
            var iset = new WRawInstructionSet(a);


            a.GetEndpoint("Ak").WriteEndpoint(WORD.FromUInt64(25ul));
            a.Commit();

            Assert.AreEqual(25ul, a.GetEndpoint("Ak").ReadEndpoint(64).ToUInt64());

            a.GetEndpoint("magS").WriteEndpoint(WORD.FromUInt64(1ul));

            Assert.AreEqual(1ul, a.GetEndpoint("magS").ReadEndpoint(64).ToUInt64());

            a.ExecSignals(iset.Instructions[0].Cycles[0][0]);

            a.Commit();

            Assert.AreEqual(26ul, a.GetEndpoint("Ak").ReadEndpoint(64).ToUInt64());
        }
    }
}

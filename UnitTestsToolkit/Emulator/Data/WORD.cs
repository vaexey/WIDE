using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace ToolkitUnitTests.Emulator.Data
{
    [TestClass]
    public class WORDTest
    {
        [TestMethod]
        public void Initialization()
        {
            var src1 = Encoding.ASCII.GetBytes("test123");
            
            var src2 = new byte[] { 0x01, 0x33, 0x55 };
            var src3 = new byte[] { 0x01, 0x33, 0x15 };

            var w1 = (WORD)src1;
            var w2 = WORD.FromBytes(src2, 22);

            var dest1 = w1.ToBytes();
            var dest2 = w2.ToBytes();

            Assert.IsTrue(dest1.SequenceEqual(src1));
            Assert.IsTrue(dest2.SequenceEqual(src3));
        }

        [TestMethod]
        public void Slice()
        {
            {
                var src = new byte[] { 0xca, 0xfe };
                var dest = new byte[] { 0xca };

                var w = src.ToWord().Slice(0, 8);

                Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
            }
            {
                //                 0x153301
                //            v           v 0   
                // 00010101 00110011 00000001
                //             10011 000000
                //                   0x04C0
                var src = new byte[] { 0x01, 0x33, 0x15 };
                var dest = new byte[] { 0xC0, 0x04 };

                var w = src.ToWord().Slice(2, 13);

                Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
            }
            {
                var src = new byte[] { 0xde, 0xad };
                var dest = new byte[] { 0xbb, 0x05 };

                var w = src.ToWord().Slice(3, 15);

                Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
            }
            {
                var src = 0xef;
                var dest = new byte[] { 0b111 };

                var w = WORD.FromUInt64((ulong)src).Slice(5, 8);

                Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
            }
        }

        [TestMethod]
        public void ToString_()
        {
            {
                var src = new byte[] { 0xef, 0xbe, 0xad, 0xde };

                var w = src.ToWord().ToString(16);

                Assert.AreEqual("deadbeef", w);
            }
            {
                var src = new byte[] { 0xef, 0xbe, 0xad, 0xde };

                var w = src.ToWord().ToString(10, " ");

                Assert.AreEqual("222 173 190 239", w);
            }
        }

        [TestMethod]
        public void Write1()
        {
            var src1 = new byte[] { 0x00, 0xde };
            var src2 = new byte[] { 0xff };
            var dest = new byte[] { 0x38, 0xde };

            var w = src1.ToWord();

            w.Write(src2.ToWord(3), 3);

            Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
        }

        [TestMethod]
        public void Write2()
        {
            var src1 = new byte[] { 0x00, 0x00 };
            var src2 = new byte[] { 0xff };
            var dest = new byte[] { 0xf8, 0x07 };

            var w = src1.ToWord();

            w.Write(src2.ToWord(), 3);

            Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
        }

        [TestMethod]
        public void Write3()
        {
            var src1 = new byte[] { 0xaa, 0xaa, 0xaa, 0xaa, 0xaa };
            var src2 = new byte[] { 0x12, 0x34, 0x56};
            var dest = new byte[] { 0x2a, 0x41, 0x63, 0xa5, 0xaa };

            var w = src1.ToWord();

            w.Write(src2.ToWord(), 4);

            Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
        }

        [TestMethod]
        public void Write4()
        {
            var src1 = WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul, 64);
            var src2 = WORD.FromUInt64(0xFFFFFFFF00000000ul, 64);
            var dest = WORD.FromUInt64(0xFFFFFFFF00000000ul, 64);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write5()
        {
            var src1 = WORD.FromUInt64(0xdeadbeefcafebabeul, 64);
            var src2 = WORD.FromUInt64(0xc0deb00ful, 31);
            var dest = WORD.FromUInt64(0xDEADBEE81BD601FEul, 64);

            src1.Write(src2, 5);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write6()
        {
            var src1 = WORD.FromUInt64(0x2B1B0BF83531C75Ful, 64);
            var src2 = WORD.FromUInt64(0x2C68BEED71735C4ul, 60);
            var dest = WORD.FromUInt64(0xB1A2FBB5C5CD713ul, 64);

            src1.Write(src2, 2);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write7()
        {
            var src1 = WORD.FromUInt64(0x0000000000000000ul, 64);
            var src2 = WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul, 64);
            var dest = WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul, 64);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write8()
        {
            var src1 = WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul, 64);
            var src2 = WORD.FromUInt64(0x0000000000000000ul, 32);
            var dest = WORD.FromUInt64(0xFFFFFFFF00000000ul, 64);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write9()
        {
            var src1 = WORD.FromUInt64(0xFFFFFFFFFFFFFFFFul, 3);
            var src2 = WORD.FromUInt64(0x0000000000000000ul, 64);
            var dest = WORD.FromUInt64(0x0000000000000000ul, 3);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write10()
        {
            var src1 = WORD.FromUInt64(0x60, 8);
            var src2 = WORD.FromUInt64(0xAul, 5);
            var dest = WORD.FromUInt64(0x6Aul, 8);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Write11()
        {
            var src1 = WORD.FromUInt64(0xf0, 8);
            var src2 = WORD.FromUInt64(0x1ul, 8);
            var dest = WORD.FromUInt64(0x1ul, 8);

            src1.Write(src2, 0);

            Assert.IsTrue(src1 == dest);
        }

        [TestMethod]
        public void Extend()
        {
            {
                var src1 = new byte[] { 0x2a, 0x41, 0x63, 0xa5, 0xaa };
                var dest = new byte[] { 0x2a, 0x41, 0x63, 0xa5, 0xaa, 
                                        0x00, 0x00, 0x00, 0x00, 0x00 };

                var w = src1.ToWord();

                w.ExtendBy(33);

                Assert.IsTrue(w.ToBytes().SequenceEqual(dest));
                Assert.AreEqual(73, w.Width);
            }
        }

        [TestMethod]
        public void Sum()
        {
            {
                var w1 = WORD.FromUInt64(1241ul);
                var w2 = WORD.FromUInt64(2333ul);

                var sum = w1 + w2;

                Assert.AreEqual(3574ul, sum.ToUInt64());
                Assert.AreEqual(1241ul, w1.ToUInt64());
            }
        }

        [TestMethod]
        public void Subtraction1()
        {
            var w1 = WORD.FromUInt64(150ul);
            var w2 = WORD.FromUInt64(300ul);

            var exp = WORD.FromUInt64(unchecked((ulong)-150L));
            var res = w1 - w2;

            Assert.IsTrue(exp == res);
        }

        [TestMethod]
        public void Subtraction2()
        {
            var w1 = WORD.FromUInt64(150ul);
            var w2 = WORD.FromUInt64(150ul);

            var exp = WORD.FromUInt64(0ul);
            var res = w1 - w2;

            Assert.IsTrue(exp == res);
        }

        [TestMethod]
        public void Subtraction3()
        {
            var w1 = WORD.FromUInt64(150ul);
            var w2 = WORD.FromUInt64(50ul);

            var exp = WORD.FromUInt64(100ul);
            var res = w1 - w2;

            Assert.IsTrue(exp == res);
        }

        [TestMethod]
        public void Subtraction4()
        {
            var w1 = WORD.FromUInt64(150ul);
            var w2 = WORD.FromUInt64(unchecked((ulong)-200L));

            var exp = WORD.FromUInt64(350ul);
            var res = w1 - w2;

            Assert.IsTrue(exp == res);
        }

        [TestMethod]
        public void Invert()
        {
            var w1 = WORD.FromUInt64(0b0110110111011UL, 14);
            var w2 = WORD.FromUInt64(0b11001001000100UL, 14);

            w1.Invert();

            Assert.IsTrue(w1 == w2);
        }

        [TestMethod]
        public void Empty()
        {
            var w1 = WORD.Zero(0);

            Assert.AreEqual(0, w1.Width);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Data;

namespace ToolkitUnitTests.Emulator.Data
{
    [TestClass]
    public class SingleMemoryTest
    {
        [TestMethod]
        public void RW1()
        {
            Memory mem = new SingleMemory(256);

            Assert.AreEqual(256, mem.GetSize());
            Assert.AreEqual(8, mem.GetWordSize());

            var r1 = mem.Read(0, 256);

            Assert.AreEqual(256, r1.Count());

            foreach (var w in r1)
                Assert.AreEqual(0ul, w.ToUInt64());

            for (int i = 1; i < 256; i += 2)
                mem.Write(i, WORD.FromUInt64(0xff, 8));

            var r2 = mem.Read(0, 256).ToArray();

            for(int i = 0; i < 256; i++)
            {
                Assert.AreEqual(255 * (i % 2), (int)r2[i].ToUInt64());
            }
        }

        [TestMethod]
        public void OutOfBounds()
        {
            Memory mem = new SingleMemory(256);

            Assert.ThrowsException<MemoryException>(() =>
            {
                mem.Read(-1);
            });

            Assert.ThrowsException<MemoryException>(() =>
            {
                mem.Read(256);
            });


            Assert.ThrowsException<MemoryException>(() =>
            {
                mem.Write(-1, WORD.Zero(1));
            });

            Assert.ThrowsException<MemoryException>(() =>
            {
                mem.Write(256, WORD.Zero(1));
            });
        }
    }
}

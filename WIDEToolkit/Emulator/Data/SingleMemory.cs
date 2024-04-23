using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Data
{
    public class SingleMemory : Memory
    {
        protected WORD memory;

        public int WordSize { get; }
        public int Size { get; }

        public override int GetSize() => Size;
        public override int GetWordSize() => WordSize;

        public SingleMemory(int size, int wordSize = 8)
        {
            Size = size;
            WordSize = wordSize;

            memory = WORD.Zero(size * wordSize);
        }

        public override WORD Read(int address)
        {
            CheckBounds(address);

            int bitwise = address * WordSize;

            return memory.Slice(bitwise, bitwise + WordSize);
        }

        public override void Write(int address, WORD value)
        {
            CheckBounds(address);

            int bitwise = address * WordSize;

            if(value.Width == WordSize)
            {
                memory.Write(value, bitwise);

                return;
            }

            memory.Write(
                value.Slice(0, WordSize), bitwise
            );

            SetChangedFlag();
        }
    }
}

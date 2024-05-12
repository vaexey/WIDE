using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDE.View.Editors
{
    public class EditableMemoryWORD
    {
        private Memory Mem;

        private ulong MinValue = 0;
        private ulong MaxValue;
        private int HexAddressWidth;

        public EditableMemoryWORD(Memory mem, int address)
        {
            Mem = mem;
            MaxValue = (ulong)Math.Pow(2, mem.GetWordSize()) - 1;
            HexAddressWidth = Convert.ToString(mem.GetSize() - 1, 16).Length;
            Address = address;
        }

        public int Address { get; }
        public string HexAddress => Convert.ToString(Address, 16).PadLeft(HexAddressWidth, '0');

        public ulong UInt64
        {
            get => Mem.Read(Address).ToUInt64();
            set => Mem.Write(Address, 
                WORD.FromUInt64(
                        Math.Clamp(value, MinValue, MaxValue)
                    )
                );
        }

        public string HexString
        {
            get => Convert.ToString(unchecked((long)UInt64), 16).PadLeft(2, '0');
            set
            {
                try
                {
                    UInt64 = Convert.ToUInt64(value, 16);
                }
                catch
                { }
            }
        }
    }
}

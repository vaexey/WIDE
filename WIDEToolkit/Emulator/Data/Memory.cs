using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Flow;

namespace WIDEToolkit.Emulator.Data
{
    public abstract class Memory
    {
        public string? DivisionName { get; set; } = null;

        public abstract int GetSize();
        public abstract int GetWordSize();


        public abstract WORD Read(int address);
        public abstract void Write(int address, WORD value);

        public virtual bool HasChanged { get; set; } = false;

        public virtual bool CheckChangedFlag()
        {
            if(HasChanged)
            {
                HasChanged = false;

                return true;
            }

            return false;
        }

        protected virtual void SetChangedFlag()
        {
            HasChanged = true;
        }

        protected virtual void CheckBounds(int address)
        {
            if ( address < 0 || address >= GetSize())
                throw new MemoryException($"Address {address} out of memory bounds {GetSize()}");
        }

        public virtual IEnumerable<WORD> Read(int address, int length)
        {
            while (length-- != 0)
            {
                yield return Read(address);

                address++;
            }
        }

        public virtual void Write(int address, IEnumerable<WORD> value)
        {
            foreach(var w in value)
            {
                Write(address, w);

                address++;
            }
        }
    }
}

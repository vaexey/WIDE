using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.Controller;

namespace WIDE.View.Controls
{
    public class SimpleByteProvider : IByteProvider
    {
        private bool _hasChanges;

        public List<byte> Bytes;

        public long Length => Bytes.Count;

        public event EventHandler? Changed;

        public event EventHandler? LengthChanged;

        // TODO: proper event args
        public event EventHandler<Tuple<long, byte>>? Write;

        public SimpleByteProvider(byte[] data)
            : this(new List<byte>(data))
        {
        }

        public SimpleByteProvider(List<byte> bytes)
        {
            Bytes = bytes;
        }

        public void OnChanged(EventArgs? e = null)
        {
            _hasChanges = true;
            if (Changed != null)
            {
                Changed(this, e ?? EventArgs.Empty);
            }
        }

        public void OnLengthChanged(EventArgs? e = null)
        {
            if (LengthChanged != null)
            {
                LengthChanged(this, e ?? EventArgs.Empty);
            }
        }

        protected void OnWrite(Tuple<long, byte> value)
        {
            if (Write != null)
                Write(this, value);
        }

        public bool HasChanges()
        {
            return _hasChanges;
        }

        public void ApplyChanges()
        {
            _hasChanges = false;
        }

        public byte ReadByte(long index)
        {
            return Bytes[(int)index];
        }

        public void WriteByte(long index, byte value)
        {
            //_bytes[(int)index] = value;
            //OnChanged(EventArgs.Empty);
            //throw new NotImplementedException();
            OnWrite(new(index, value));
        }

        public void DeleteBytes(long index, long length)
        {
            //int index2 = (int)Math.Max(0L, index);
            //int count = (int)Math.Min((int)Length, length);
            //_bytes.RemoveRange(index2, count);
            //OnLengthChanged(EventArgs.Empty);
            //OnChanged(EventArgs.Empty);
            throw new NotImplementedException();
            //throw new UserFriendlyException(Texts.Emulator.MemoryCannotRemove);
        }

        public void InsertBytes(long index, byte[] bs)
        {
            //_bytes.InsertRange((int)index, bs);
            //OnLengthChanged(EventArgs.Empty);
            //OnChanged(EventArgs.Empty);
            throw new NotImplementedException();
            //throw new UserFriendlyException(Texts.Emulator.MemoryCannotAdd);
        }

        public bool SupportsWriteByte()
        {
            return true;
        }

        public bool SupportsInsertBytes()
        {
            return false;
        }

        public bool SupportsDeleteBytes()
        {
            return false;
        }
    }
}

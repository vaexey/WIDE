using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDEToolkit.Emulator.Data
{
    /// <summary>
    /// A wrapper for byte[] that allows direct bit manipulation
    /// </summary>
    public class WORD
    {
        private byte[] _bytes;
        private int _width;

        /// <summary>
        /// Bitwise word width
        /// </summary>
        public int Width => _width;

        private WORD(byte[] bytes, int width)
        {
            _bytes = bytes;
            _width = width;
        }

        public byte[] ToBytes()
        {
            return _bytes;
        }

        public ulong ToUInt64()
        {
            //return BitConverter.ToUInt64(_bytes, 0);
            int idx = _bytes.Length - 1;
            ulong val = _bytes[idx--];

            while (--idx > -1)
            {
                val = (val << 8) | _bytes[idx];
            }

            return val;
        }

        public WORD Slice(int start, int end)
        {
            var o = new byte[(int)Math.Ceiling((end - start) / 8d)];

            int offset = (int)Math.Floor(start / 8d);
            int lb = start % 8;
            int hb = 8 - start % 8;

            int i;
            for (i = 0; i < o.Length - 1; i++)
            {
                o[i] = (byte)(
                    (_bytes[offset + i] >> lb) +
                    (_bytes[offset + i + 1] << hb) & 0xFF
                );
            }
            for (; i < o.Length; i++)
            {
                o[i] = (byte)(
                    (_bytes[offset + i] >> lb)
                );
            }

            int xb = end % 8;

            //o[i - 1] = (byte)(o[i - 1] & (0xFF >> xb));

            return FromBytes(o, end - start);
        }

        public byte[] SliceBytes(int start, int end)
        {
            return Slice(start, end).ToBytes();
        }

        public static WORD FromBytes(byte[] bytes, int width = 0)
        {
            if (width == 0)
                width = bytes.Length * 8;

            var ceil = width;
            if (width % 8 != 0)
                ceil += 8 - width % 8;

            if (ceil / 8 != bytes.Length)
            {
                Array.Resize(ref bytes, ceil / 8);
            }

            if (width % 8 != 0)
            {
                bytes[bytes.Length - 1] =
                    (byte)(bytes[bytes.Length - 1] & ~(0xFF << width % 8));
            }

            return new WORD(bytes, width);
        }

        public static WORD FromUInt64(ulong value, int width = 0)
        {
            return FromBytes(BitConverter.GetBytes(value), width);
        }

        public static WORD Zero(int width)
        {
            var ceil = width;
            if (width % 8 != 0)
                ceil += 8 - width % 8;

            return FromBytes(new byte[ceil / 8], width);
        }

        public static WORD One(int width)
        {
            var ceil = width;
            byte[] arr;

            if (width % 8 != 0)
            {
                ceil += 8 - width % 8;

                arr = new byte[ceil / 8];

                for (int i = 0; i < arr.Length - 1; i++)
                    arr[i] = 255;

                arr[arr.Length - 1] = (byte)(255 >> (8 - width % 8));
            }
            else
            {
                arr = new byte[ceil / 8];

                for(int i = 0; i < arr.Length; i++)
                    arr[i] = 255;
            }

            return FromBytes(arr, width);
        }

        public void Write(WORD w, int start)
        {
            if(w.Width > Width - start)
            {
                w = w.Slice(0, Width - start + 1);
            }

            int idx1 = start / 8;
            int idx2 = (start + w._width - 1) / 8;

            int woff = start % 8;
            int wmask = (0xff << woff) & 0xff;

            if (idx1 == idx2)
                wmask &= (0xff >> (8 - woff - w._width)) & 0xff;

            _bytes[idx1] = (byte)(
                (_bytes[idx1] & ~wmask) |
                ((w._bytes[0] << woff) & wmask)
             );

            if (idx1 == idx2)
            {
                return;
            }

            for (int i = idx1 + 1; i < idx2; i++)
            {
                int j = i - idx1;
                int bit = j * 8 - woff - 1;
                int widx = bit / 8;

                _bytes[i] = (byte)(
                    (w._bytes[widx] >> (8-woff)) |
                    (w._bytes[widx + 1] << woff)
                );
            }

            int eoff = (start + w._width) % 8;
            if (eoff == 0) eoff = 8;

            int emask = ~(0xff << eoff) & 0xff;

            int eidx = (8 * (idx2 - idx1) - woff) / 8;

            if (eidx + 1 == w._bytes.Length)
            {
                _bytes[idx2] = (byte)(
                    (w._bytes[eidx] >> ((8 - woff) % 8)) |
                    (_bytes[idx2] & ~emask)
                );

                return;
            }

            _bytes[idx2] = (byte)(
                (w._bytes[eidx] >> (8-woff)) |
                ((w._bytes[eidx + 1] << woff) & emask) |
                (_bytes[idx2] & ~emask)
            );
        }

        public void Write(WORD w, int start, int ss, int se = 0)
        {
            if (se == 0)
                se = w._width;

            Write(w.Slice(ss, se), start);
        }

        public void ExtendBy(int by)
        {
            var width = _width + by;

            ExtendTo(width);
        }

        public void ExtendTo(int width)
        {
            var ceil = width;
            if (width % 8 != 0)
                ceil += 8 - width % 8;

            if (ceil / 8 != _bytes.Length)
            {
                Array.Resize(ref _bytes, ceil / 8);
            }

            _width = width;
        }

        public void Add(WORD value)
        {
            int min = Math.Min(_bytes.Length, value._bytes.Length);

            int carry = 0;
            for (int i = 0; i < min; i++)
            {
                int sum = _bytes[i] + value._bytes[i] + carry;
                carry = sum >> 8;

                _bytes[i] = (byte)sum;
            }
        }

        public void Subtract(WORD value)
        {
            if(value.Width == _width)
            {
                //2s compliment inversion
                var twocpl = value.Clone();
                twocpl.Invert();
                twocpl.Add(FromUInt64(1ul, value._width));

                Add(twocpl);

                return;
            }

            // TODO
            throw new Exception("Cannot subtract WORDs with different widths");
        }

        public void Invert()
        {
            for(int i = 0; i < _bytes.Length; i++)
            {
                _bytes[i] = (byte)~_bytes[i];
            }

            int mod = _width % 8;
            if (mod != 0)
            {
                int i = _bytes.Length - 1;
                _bytes[i] = (byte)(_bytes[i] & (0xff >> (8 - mod)));
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if(obj is WORD w)
            {
                if(_width != w._width)
                    return false;

                for (int i = 0; i < _bytes.Length; i++)
                    if (_bytes[i] != w._bytes[i])
                        return false;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return ToString(16);
        }

        public string ToString(int radix, string delim = "")
        {
            int width = 0;

            if(radix == 16)
            {
                width = 2;
            } else if(radix == 2)
            {
                width = 8;
            }

            return string.Join(delim, _bytes.Reverse().Select(
                    b => Convert.ToString(b, radix).PadLeft(width, '0')
                ).ToArray());
        }

        public WORD Clone()
        {
            return new WORD((byte[])_bytes.Clone(), _width);
        }

        public static explicit operator WORD(byte[] bytes)
        {
            return FromBytes(bytes);
        }

        public static bool operator ==(WORD? w1, WORD? w2)
        {
            if (w1 is null)
                return w2 is null;

            return w1.Equals(w2);
        }

        public static bool operator !=(WORD? w1, WORD? w2)
        {
            return !(w1 == w2);
        }

        public static WORD operator +(WORD w1, WORD w2)
        {
            var w = w1.Clone();

            w.Add(w2);

            return w;
        }
        public static WORD operator -(WORD w1, WORD w2)
        {
            var w = w1.Clone();

            w.Subtract(w2);

            return w;
        }

        public WORD this[int start, int end]
        {
            get => Slice(start, end);
            // TODO: unify implementation
            //set => Write(value, start, 0, end - start);
        }

        public WORD this[int index]
        {
            get => Slice(0, index);
            set => Write(value, index);
        }
    }

    public static class WORDExtensions
    {
        public static WORD ToWord(this byte[] arr, int width = 0)
        {
            return WORD.FromBytes(arr, width);
        }
    }


}

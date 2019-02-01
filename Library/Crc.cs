using System;
using System.Text;

namespace InvertedTomato.Checksum
{
    /// https://github.com/meetanthony/crccsharp
    public class Crc
    {
        public readonly Int32 Width;
        public readonly UInt64 Polynomial;
        public readonly UInt64 Initial;
        public readonly Boolean IsInputReflected;
        public readonly Boolean IsOutputReflected;
        public readonly UInt64 OutputXor;
        public readonly UInt64 Check;


        private readonly UInt64 Mask;

        private readonly UInt64[] Lookup = new UInt64[256];



        public Crc(Int32 width, UInt64 polynomial, UInt64 initial, Boolean isInputReflected, Boolean isOutputReflected, UInt64 outputXor, UInt64 check = 0)
        {
            if ((width % 8) != 0 || width < 8 || width > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Must be a multiple of 8 and between 8 and 64.");
            }

            // Store values
            Width = width;
            Polynomial = polynomial;
            Initial = initial;
            IsInputReflected = isInputReflected;
            IsOutputReflected = isOutputReflected;
            OutputXor = outputXor;
            Check = check;

            // Compute mask
            Mask = UInt64.MaxValue >> (64 - width);

            // Create lookup table
            for (var i = 0; i < Lookup.Length; i++)
            {
                UInt64 r = (UInt64)i;
                if (isInputReflected)
                {
                    r = ReverseBits(r, width);
                }
                else if (width > 8)
                {
                    r <<= (width - 8);
                }

                UInt64 lastBit = (1ul << (width - 1));

                for (var j = 0; j < 8; j++)
                {
                    if ((r & lastBit) != 0)
                    {
                        r = ((r << 1) ^ polynomial);
                    }
                    else
                    {
                        r <<= 1;
                    }
                }

                if (isInputReflected)
                {
                    r = ReverseBits(r, width);
                }

                Lookup[i] = r & Mask;
            }
        }

        public byte[] ComputeBytes(String input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return ComputeBytes(Encoding.ASCII.GetBytes(input));
        }

        public byte[] ComputeBytes(Byte[] input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }


            return BitConverter.GetBytes(Compute(input));
        }

        public UInt64 Compute(String input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Compute(Encoding.ASCII.GetBytes(input));
        }

        public UInt64 Compute(Byte[] input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }


            return Compute(input, 0, input.Length);
        }

        private UInt64 Compute(Byte[] data, Int32 offset, Int32 length)
        {
            var crc = Initial;

            if (IsOutputReflected)
            {
                for (var i = offset; i < offset + length; i++)
                {
                    crc = (Lookup[(crc ^ data[i]) & 0xFF] ^ (crc >> 8));
                    crc &= Mask;
                }
            }
            else
            {
                Int32 toRight = (Width - 8);
                toRight = toRight < 0 ? 0 : toRight;
                for (Int32 i = offset; i < offset + length; i++)
                {
                    crc = (Lookup[((crc >> toRight) ^ data[i]) & 0xFF] ^ (crc << 8));
                    crc &= Mask;
                }
            }

            return crc;
        }





        private static UInt64 ReverseBits(UInt64 value, Int32 valueLength)
        {
            UInt64 output = 0;

            for (var i = valueLength - 1; i >= 0; i--)
            {
                output |= (value & 1) << i;
                value >>= 1;
            }

            return output;
        }
    }
}

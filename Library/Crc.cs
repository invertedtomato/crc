using System;
using System.Text;

namespace InvertedTomato.Checksum
{
    /// Based around https://github.com/meetanthony/crccsharp
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
                var r = (UInt64)i;
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

        public Byte[] Compute(Byte[] input)
        {
            return Compute(input, 0, input.Length);
        }

        private Byte[] Compute(Byte[] input, Int32 offset, Int32 length)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var crc = Initial;

            if (IsOutputReflected)
            {
                for (var i = offset; i < offset + length; i++)
                {
                    crc = (Lookup[(crc ^ input[i]) & 0xFF] ^ (crc >> 8));
                    crc &= Mask;
                }
            }
            else
            {
                var toRight = (Width - 8);
                toRight = toRight < 0 ? 0 : toRight;
                for (var i = offset; i < offset + length; i++)
                {
                    crc = (Lookup[((crc >> toRight) ^ input[i]) & 0xFF] ^ (crc << 8));
                    crc &= Mask;
                }
            }

            // Apply output XOR
            crc ^= OutputXor;

            // Convert result to correct-sized byte array
            var result = BitConverter.GetBytes(crc);  // TODO: This all smells bad
            if (BitConverter.IsLittleEndian)
            {
                Array.Resize(ref result, Width / 8);
                Array.Reverse(result);
            }
            else
            {
                Array.Resize(ref result, Width / 8);
            }

            return result;
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

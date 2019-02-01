using System;
using System.Text;
using InvertedTomato.Checksum.Extensions;

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

        private readonly UInt64[] PrecomputationTable = new UInt64[256];

        private UInt64 Current;


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
            for (var i = 0; i < PrecomputationTable.Length; i++)
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

                PrecomputationTable[i] = r & Mask;
            }

            Clear();
        }


        public void Append(Byte[] input)
        {
            Append(input, 0, input.Length);
        }

        public void Append(Byte[] input, Int32 offset, Int32 count)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            for (var i = offset; i < offset + count; i++)
            {
                Append(input[i]);
            }
        }

        public void Append(Byte input)
        {
            if (IsOutputReflected)
            {
                Current = (PrecomputationTable[(Current ^ input) & 0xFF] ^ (Current >> 8));
            }
            else
            {
                var toRight = (Width - 8); // TODO: don't do on every Append
                toRight = toRight < 0 ? 0 : toRight;// TODO: don't do on every Append

                Current = (PrecomputationTable[((Current >> toRight) ^ input) & 0xFF] ^ (Current << 8));
            }

            Current &= Mask; // TODO: required on every cycle?
        }

        public UInt64 ToUInt64()
        {
            // Apply output XOR
            return Current ^ OutputXor;
        }

        public Byte[] ToByteArray()
        {
            var output = ToUInt64();

            // Convert result to correct-sized byte array
            var result = BitConverter.GetBytes(output);  // TODO: This all smells bad
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

        public String ToHexString()
        {
            return ToByteArray().ToHexString();
        }
        public Byte[] ComputeByteArray(Byte[] input)
        {
            Append(input);
            return ToByteArray();
        }

        public void Clear()
        {
            // Initialise current
            Current = IsOutputReflected ? ReverseBits(Initial,Width) : Initial;
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

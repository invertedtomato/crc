using System;
using System.Text;
using InvertedTomato.Checksum.Extensions;

namespace InvertedTomato.Checksum
{
    /// <summary>
    ///Library for computing CRCs of any scheme in sizes of 8, 16, 24, 32, 40, 48, 56 and 64bits. 
    /// </summary>
    /// <remarks>
    /// Based loosely on https://github.com/meetanthony/crccsharp and drawing from the fantastic work from R. Williams http://www.ross.net/crc/download/crc_v3.txt
    /// </summary>
    public class Crc
    {
        ///<summary>Name given to the algorithm.</summary>
        public readonly String Name;

        /// <summary> Width of the algorithm expressed in bits.</summary>
        /// <remarks>This is one less bit than the width of the Polynomial.</remarks>
        public readonly Int32 Width;

        /// <summary>The polynomial used for the CRC calculation, omitting the top bit. </summary>
        /// <remarks>The top bit of the poly should be omitted. For example, if the poly is 10110, you should specify 0x06. Also, an important aspect of this parameter is that it represents the unreflected poly; the bottom bit of this parameter is always the LSB of the divisor during the division regardless of whether the algorithm being modelled is reflected.</remarks>
        public readonly UInt64 Polynomial;

        /// <summary>The initial value of the register when the algorithm starts.</summary>
        public readonly UInt64 Initial;

        /// <summary>If the input is to be reflected before processing</summary>
        /// <remarks>If it is FALSE, input bytes are processed with bit 7 being treated as the most significant bit (MSB) and bit 0 being treated as the least significant bit. If this parameter is TRUE, each byte is reflected before being processed.</remarks>
        public readonly Boolean IsInputReflected;

        /// <summary>Is the output to be reflected.</summary>
        /// <remarks>If it is set to FALSE, the final value in the register is fed into the OutputXor stage directly, otherwise, if this parameter is TRUE, the final register value is reflected first.</remarks>
        public readonly Boolean IsOutputReflected;

        /// <summary>This value is XORed to the final register value (after the IsOutputReflected stage) before the value is returned as the official checksum.</summary>
        public readonly UInt64 OutputXor;

        /// <summary>The checksum obtained when the ASCII string "123456789" is fed through the specified algorithm (i.e. 0x313233...).</summary>
        /// <remarks>This field is not strictly part of the definition, and, in the event of an inconsistency between this field and the other field, the other fields take precedence. This field is a check value that can be used as a weak validator of implementations of the algorithm.</remarks>
        public readonly UInt64 Check;


        private readonly UInt64 Mask;

        private readonly UInt64[] PrecomputationTable = new UInt64[256];

        private UInt64 Current;


        public Crc(String name, Int32 width, UInt64 polynomial, UInt64 initial, Boolean isInputReflected, Boolean isOutputReflected, UInt64 outputXor, UInt64 check = 0)
        {
            if ((width % 8) != 0 || width < 8 || width > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Must be a multiple of 8 and between 8 and 64.");
            }

            // Store values
            Name = name;
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


        /// <summary>Compute the hash of a byte array. This can be called multiple times for consecutive blocks of input.</summary>
        public void Append(Byte[] input)
        {
            Append(input, 0, input.Length);
        }

        /// <summary>Compute the hash of a byte array with a defined offset and count. This can be called multiple times for consecutive blocks of input.</summary>
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

        /// <summary>Compute the hash of a byte. This can be called multiple times for consecutive bytes.</summary>

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

        /// <summary> Retrieve the CRC of the bytes that have been input so far.</summary>
        public UInt64 ToUInt64()
        {
            // Apply output XOR
            return Current ^ OutputXor;
        }


        /// <summary> Retrieve the CRC of the bytes that have been input so far.</summary>
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


        /// <summary> Retrieve the CRC of the bytes that have been input so far.</summary>
        public String ToHexString()
        {
            return ToByteArray().ToHexString();
        }

        /// <summary>Reset the state so that a new set of data can be input without being affected by previous sets.</summary>
        /// <remarks>Typically this is called after retrieving a computed CRC (using ToByteArray for exanmple) and before calling Append for a new computation run.</remarks>
        public void Clear()
        {
            // Initialise current
            Current = IsOutputReflected ? ReverseBits(Initial, Width) : Initial;
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

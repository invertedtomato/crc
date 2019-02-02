using System;
using InvertedTomato.IO.Extensions;

namespace InvertedTomato.IO
{
    /// <summary>
    ///     Library for computing CRCs of any algorithm in sizes of 8-64bits.
    /// </summary>
    /// <remarks>
    ///     Based loosely on https://github.com/meetanthony/crccsharp and drawing from the fantastic work from R. Williams
    ///     http://www.ross.net/crc/download/crc_v3.txt
    ///     </summary>
    public class Crc
    {
        /// <summary>
        ///     The checksum obtained when the ASCII string "123456789" is fed through the specified algorithm (i.e.
        ///     0x313233...).
        /// </summary>
        /// <remarks>
        ///     This field is not strictly part of the definition, and, in the event of an inconsistency between this field
        ///     and the other field, the other fields take precedence. This field is a check value that can be used as a weak
        ///     validator of implementations of the algorithm.
        /// </remarks>
        public readonly ulong Check;

        /// <summary>The initial value of the register when the algorithm starts.</summary>
        public readonly ulong Initial;

        /// <summary>If the input is to be reflected before processing</summary>
        /// <remarks>
        ///     If it is FALSE, input bytes are processed with bit 7 being treated as the most significant bit (MSB) and bit 0
        ///     being treated as the least significant bit. If this parameter is TRUE, each byte is reflected before being
        ///     processed.
        /// </remarks>
        public readonly bool IsInputReflected;

        /// <summary>Is the output to be reflected.</summary>
        /// <remarks>
        ///     If it is set to FALSE, the final value in the register is fed into the OutputXor stage directly, otherwise, if
        ///     this parameter is TRUE, the final register value is reflected first.
        /// </remarks>
        public readonly bool IsOutputReflected;


        private readonly ulong Mask;

        ///<summary>Name given to the algorithm.</summary>
        public readonly string Name;

        /// <summary>
        ///     This value is XORed to the final register value (after the IsOutputReflected stage) before the value is
        ///     returned as the official checksum.
        /// </summary>
        public readonly ulong OutputXor;

        /// <summary>The polynomial used for the CRC calculation, omitting the top bit. </summary>
        /// <remarks>
        ///     The top bit of the poly should be omitted. For example, if the poly is 10110, you should specify 0x06. Also,
        ///     an important aspect of this parameter is that it represents the unreflected poly; the bottom bit of this parameter
        ///     is always the LSB of the divisor during the division regardless of whether the algorithm being modelled is
        ///     reflected.
        /// </remarks>
        public readonly ulong Polynomial;

        private readonly ulong[] PrecomputationTable = new ulong[256];

        /// <summary> Width of the algorithm expressed in bits.</summary>
        /// <remarks>This is one less bit than the width of the Polynomial.</remarks>
        public readonly int Width;

        private ulong Current;


        public Crc(string name, int width, ulong polynomial, ulong initial, bool isInputReflected,
            bool isOutputReflected, ulong outputXor, ulong check = 0)
        {
            if (width < 8 || width > 64)
                throw new ArgumentOutOfRangeException(nameof(width), "Must be a multiple of 8 and between 8 and 64.");

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
            Mask = ulong.MaxValue >> (64 - width);

            // Create lookup table
            for (var i = 0; i < PrecomputationTable.Length; i++)
            {
                var r = (ulong) i;
                if (isInputReflected)
                    r = ReverseBits(r, width);
                else if (width > 8) r <<= width - 8;

                var lastBit = 1ul << (width - 1);

                for (var j = 0; j < 8; j++)
                    if ((r & lastBit) != 0)
                        r = (r << 1) ^ polynomial;
                    else
                        r <<= 1;

                if (isInputReflected) r = ReverseBits(r, width);

                PrecomputationTable[i] = r & Mask;
            }

            Clear();
        }


        /// <summary>Compute the hash of a byte array. This can be called multiple times for consecutive blocks of input.</summary>
        public void Append(byte[] input)
        {
            Append(input, 0, input.Length);
        }

        /// <summary>
        ///     Compute the hash of a byte array with a defined offset and count. This can be called multiple times for
        ///     consecutive blocks of input.
        /// </summary>
        public void Append(byte[] input, int offset, int count)
        {
            if (null == input) throw new ArgumentNullException(nameof(input));

            for (var i = offset; i < offset + count; i++) Append(input[i]);
        }

        /// <summary>Compute the hash of a byte. This can be called multiple times for consecutive bytes.</summary>
        public void Append(byte input)
        {
            if (IsOutputReflected)
            {
                Current = PrecomputationTable[(Current ^ input) & 0xFF] ^ (Current >> 8);
            }
            else
            {
                var toRight = Width - 8; // TODO: don't do on every Append
                toRight = toRight < 0 ? 0 : toRight; // TODO: don't do on every Append

                Current = PrecomputationTable[((Current >> toRight) ^ input) & 0xFF] ^ (Current << 8);
            }

            Current &= Mask; // TODO: required on every cycle?
        }

        /// <summary> Retrieve the CRC of the bytes that have been input so far.</summary>
        public ulong ToUInt64()
        {
            // Apply output XOR
            return Current ^ OutputXor;
        }


        /// <summary> Retrieve the CRC of the bytes that have been input so far.</summary>
        public byte[] ToByteArray()
        {
            var output = ToUInt64();

            // Convert result to correct-sized byte array
            var result = BitConverter.GetBytes(output); // TODO: This all smells bad
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
        public string ToHexString()
        {
            return ToByteArray().ToHexString();
        }

        /// <summary>Reset the state so that a new set of data can be input without being affected by previous sets.</summary>
        /// <remarks>
        ///     Typically this is called after retrieving a computed CRC (using ToByteArray for exanmple) and before calling
        ///     Append for a new computation run.
        /// </remarks>
        public void Clear()
        {
            // Initialise current
            Current = IsOutputReflected ? ReverseBits(Initial, Width) : Initial;
        }


        private static ulong ReverseBits(ulong value, int valueLength)
        {
            ulong output = 0;

            for (var i = valueLength - 1; i >= 0; i--)
            {
                output |= (value & 1) << i;
                value >>= 1;
            }

            return output;
        }
    }
}
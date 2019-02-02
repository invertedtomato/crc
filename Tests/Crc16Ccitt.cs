using System;

namespace InvertedTomato.IO
{
    public static class Crc16Ccitt // http://sanity-free.org/133/crc_16_ccitt_in_csharp.html
    {
        private const ushort Poly = 4129;
        private static readonly ushort[] Table = new ushort[256];

        static Crc16Ccitt()
        {
            // Precompute table
            ushort temp, a;
            for (var i = 0; i < Table.Length; ++i)
            {
                temp = 0;
                a = (ushort) (i << 8);
                for (var j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                        temp = (ushort) ((temp << 1) ^ Poly);
                    else
                        temp <<= 1;
                    a <<= 1;
                }

                Table[i] = temp;
            }
        }

        public static ushort ComputeInteger(byte[] bytes)
        {
            if (null == bytes) throw new ArgumentNullException(nameof(bytes));

            ushort crc = 0;
            for (var i = 0; i < bytes.Length; ++i) crc = (ushort) ((crc << 8) ^ Table[(crc >> 8) ^ (0xff & bytes[i])]);
            return crc;
        }

        public static byte[] ComputeBytes(byte[] bytes)
        {
            if (null == bytes) throw new ArgumentNullException(nameof(bytes));

            var crc = ComputeInteger(bytes);
            return BitConverter.GetBytes(crc);
        }
    }
}
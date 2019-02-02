using System;

namespace InvertedTomato.IO {
	public static class Crc16Ccitt // Rival test algorithm adapted from http://sanity-free.org/133/crc_16_ccitt_in_csharp.html
	{
		private const UInt16 Poly = 4129;
		private static readonly UInt16[] Table = new UInt16[256];

		static Crc16Ccitt() {
			// Precompute table
			UInt16 temp, a;
			for (var i = 0; i < Table.Length; ++i) {
				temp = 0;
				a = (UInt16) (i << 8);
				for (var j = 0; j < 8; ++j) {
					if (((temp ^ a) & 0x8000) != 0) {
						temp = (UInt16) ((temp << 1) ^ Poly);
					} else {
						temp <<= 1;
					}

					a <<= 1;
				}

				Table[i] = temp;
			}
		}

		public static UInt16 ComputeInteger(Byte[] bytes) {
			if (null == bytes) {
				throw new ArgumentNullException(nameof(bytes));
			}

			UInt16 crc = 0;
			for (var i = 0; i < bytes.Length; ++i) {
				crc = (UInt16) ((crc << 8) ^ Table[(crc >> 8) ^ (0xff & bytes[i])]);
			}

			return crc;
		}

		public static Byte[] ComputeBytes(Byte[] bytes) {
			if (null == bytes) {
				throw new ArgumentNullException(nameof(bytes));
			}

			var crc = ComputeInteger(bytes);
			return BitConverter.GetBytes(crc);
		}
	}
}
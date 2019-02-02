namespace InvertedTomato.IO {
	public static class CrcAlgorithm {
		public static Crc CreateCrc8() {
			return new Crc("CRC-8", 8, 0x7, 0x0, false, false, 0x0, 0xF4);
		}

		public static Crc CreateCrc8Cdma2000() {
			return new Crc("CRC-8/CDMA2000", 8, 0x9B, 0xFF, false, false, 0x0, 0xDA);
		}

		public static Crc CreateCrc8Darc() {
			return new Crc("CRC-8/DARC", 8, 0x39, 0x0, true, true, 0x0, 0x15);
		}

		public static Crc CreateCrc8DvbS2() {
			return new Crc("CRC-8/DVB-S2", 8, 0xD5, 0x0, false, false, 0x0, 0xBC);
		}

		public static Crc CreateCrc8Ebu() {
			return new Crc("CRC-8/EBU", 8, 0x1D, 0xFF, true, true, 0x0, 0x97);
		}

		public static Crc CreateCrc8ICode() {
			return new Crc("CRC-8/I-CODE", 8, 0x1D, 0xFD, false, false, 0x0, 0x7E);
		}

		public static Crc CreateCrc8Itu() {
			return new Crc("CRC-8/ITU", 8, 0x7, 0x0, false, false, 0x55, 0xA1);
		}

		public static Crc CreateCrc8Maxim() {
			return new Crc("CRC-8/MAXIM", 8, 0x31, 0x0, true, true, 0x0, 0xA1);
		}

		public static Crc CreateCrc8Rohc() {
			return new Crc("CRC-8/ROHC", 8, 0x7, 0xFF, true, true, 0x0, 0xD0);
		}

		public static Crc CreateCrc8Wcdma() {
			return new Crc("CRC-8/WCDMA", 8, 0x9B, 0x0, true, true, 0x0, 0x25);
		}

		public static Crc CreateCrc16CcittFalse() {
			return new Crc("CRC-16/CCITT-FALSE", 16, 0x1021, 0xFFFF, false, false, 0x0, 0x29B1);
		}

		public static Crc CreateCrc16Arc() {
			return new Crc("CRC-16/ARC", 16, 0x8005, 0x0, true, true, 0x0, 0xBB3D);
		}

		public static Crc CreateCrc16AugCcitt() {
			return new Crc("CRC-16/AUG-CCITT", 16, 0x1021, 0x1D0F, false, false, 0x0, 0xE5CC);
		}

		public static Crc CreateCrc16Buypass() {
			return new Crc("CRC-16/BUYPASS", 16, 0x8005, 0x0, false, false, 0x0, 0xFEE8);
		}

		public static Crc CreateCrc16Cdma2000() {
			return new Crc("CRC-16/CDMA2000", 16, 0xC867, 0xFFFF, false, false, 0x0, 0x4C06);
		}

		public static Crc CreateCrc16Dds110() {
			return new Crc("CRC-16/DDS-110", 16, 0x8005, 0x800D, false, false, 0x0, 0x9ECF);
		}

		public static Crc CreateCrc16DectR() {
			return new Crc("CRC-16/DECT-R", 16, 0x589, 0x0, false, false, 0x1, 0x7E);
		}

		public static Crc CreateCrc16DectX() {
			return new Crc("CRC-16/DECT-X", 16, 0x589, 0x0, false, false, 0x0, 0x7F);
		}

		public static Crc CreateCrc16Dnp() {
			return new Crc("CRC-16/DNP", 16, 0x3D65, 0x0, true, true, 0xFFFF, 0xEA82);
		}

		public static Crc CreateCrc16En13757() {
			return new Crc("CRC-16/EN-13757", 16, 0x3D65, 0x0, false, false, 0xFFFF, 0xC2B7);
		}

		public static Crc CreateCrc16Genibus() {
			return new Crc("CRC-16/GENIBUS", 16, 0x1021, 0xFFFF, false, false, 0xFFFF, 0xD64E);
		}

		public static Crc CreateCrc16Maxim() {
			return new Crc("CRC-16/MAXIM", 16, 0x8005, 0x0, true, true, 0xFFFF, 0x44C2);
		}

		public static Crc CreateCrc16Mcrf4Xx() {
			return new Crc("CRC-16/MCRF4XX", 16, 0x1021, 0xFFFF, true, true, 0x0, 0x6F91);
		}

		public static Crc CreateCrc16Riello() {
			return new Crc("CRC-16/RIELLO", 16, 0x1021, 0xB2AA, true, true, 0x0, 0x63D0);
		}

		public static Crc CreateCrc16T10Dif() {
			return new Crc("CRC-16/T10-DIF", 16, 0x8BB7, 0x0, false, false, 0x0, 0xD0DB);
		}

		public static Crc CreateCrc16Teledisk() {
			return new Crc("CRC-16/TELEDISK", 16, 0xA097, 0x0, false, false, 0x0, 0xFB3);
		}

		public static Crc CreateCrc16Tms37157() {
			return new Crc("CRC-16/TMS37157", 16, 0x1021, 0x89EC, true, true, 0x0, 0x26B1);
		}

		public static Crc CreateCrc16Usb() {
			return new Crc("CRC-16/USB", 16, 0x8005, 0xFFFF, true, true, 0xFFFF, 0xB4C8);
		}

		public static Crc CreateCrcA() {
			return new Crc("CRC-A", 16, 0x1021, 0xC6C6, true, true, 0x0, 0xBF05);
		}

		public static Crc CreateCrc16Kermit() {
			return new Crc("CRC-16/KERMIT", 16, 0x1021, 0x0, true, true, 0x0, 0x2189);
		}

		public static Crc CreateCrc16Modbus() {
			return new Crc("CRC-16/MODBUS", 16, 0x8005, 0xFFFF, true, true, 0x0, 0x4B37);
		}

		public static Crc CreateCrc16X25() {
			return new Crc("CRC-16/X-25", 16, 0x1021, 0xFFFF, true, true, 0xFFFF, 0x906E);
		}

		public static Crc CreateCrc16Xmodem() {
			return new Crc("CRC-16/XMODEM", 16, 0x1021, 0x0, false, false, 0x0, 0x31C3);
		}

		public static Crc CreateCrc24() {
			return new Crc("CRC-24", 24, 0x864CFB, 0xB704CE, false, false, 0x0, 0x21CF02);
		}

		public static Crc CreateCrc24FlexrayA() {
			return new Crc("CRC-24/FLEXRAY-A", 24, 0x5D6DCB, 0xFEDCBA, false, false, 0x0, 0x7979BD);
		}

		public static Crc CreateCrc24FlexrayB() {
			return new Crc("CRC-24/FLEXRAY-B", 24, 0x5D6DCB, 0xABCDEF, false, false, 0x0, 0x1F23B8);
		}

		public static Crc CreateCrc32() {
			return new Crc("CRC-32", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xCBF43926);
		}

		public static Crc CreateCrc32Bzip2() {
			return new Crc("CRC-32/BZIP2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0xFFFFFFFF, 0xFC891918);
		}

		public static Crc CreateCrc32C() {
			return new Crc("CRC-32C", 32, 0x1EDC6F41, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xE3069283);
		}

		public static Crc CreateCrc32D() {
			return new Crc("CRC-32D", 32, 0xA833982B, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0x87315576);
		}

		public static Crc CreateCrc32Jamcrc() {
			return new Crc("CRC-32/JAMCRC", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0x00000000, 0x340BC6D9);
		}

		public static Crc CreateCrc32Mpeg2() {
			return new Crc("CRC-32/MPEG-2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7);
		}

		public static Crc CreateCrc32Posix() {
			return new Crc("CRC-32/POSIX", 32, 0x04C11DB7, 0x00000000, false, false, 0xFFFFFFFF, 0x765E7680);
		}

		public static Crc CreateCrc32Q() {
			return new Crc("CRC-32Q", 32, 0x814141AB, 0x00000000, false, false, 0x00000000, 0x3010BF7F);
		}

		public static Crc CreateCrc32Xfer() {
			return new Crc("CRC-32/XFER", 32, 0x000000AF, 0x00000000, false, false, 0x00000000, 0xBD0BE338);
		}

		public static Crc CreateCrc40Gsm() {
			return new Crc("CRC-40/GSM", 40, 0x4820009, 0x0, false, false, 0xFFFFFFFFFF, 0xD4164FC646);
		}

		public static Crc CreateCrc64() {
			return new Crc("CRC-64", 64, 0x42F0E1EBA9EA3693, 0x00000000, false, false, 0x00000000, 0x6C40DF5F0B497347);
		}

		public static Crc CreateCrc64We() {
			return new Crc("CRC-64/WE", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, false, false, 0xFFFFFFFFFFFFFFFF,
				0x62EC59E3F1A4F00A);
		}

		public static Crc CreateCrc64Xz() {
			return new Crc("CRC-64/XZ", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, true, true, 0xFFFFFFFFFFFFFFFF,
				0x995DC9BBDF1939FA);
		}
	}
}
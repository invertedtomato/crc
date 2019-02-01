namespace InvertedTomato.Checksum
{
    public static class CrcSpecification
    {
        public static Crc CreateCrc8()
        {
            return new Crc(8, 0x7, 0x00, false, false, 0x00, 0xF4);
        }

        public static Crc CreateCrc8Cdma2000()
        {
            return new Crc(8, 0x9B, 0xFF, false, false, 0x00, 0xDA);
        }

        public static Crc CreateCrc8Darc()
        {
            return new Crc(8, 0x39, 0x00, true, true, 0x00, 0x15);
        }

        public static Crc CreateCrc8DvbS2()
        {
            return new Crc(8, 0xD5, 0x00, false, false, 0x00, 0xBC);
        }

        public static Crc CreateCrc8Ebu()
        {
            return new Crc(8, 0x1D, 0xFF, true, true, 0x00, 0x97);
        }

        public static Crc CreateCrc8ICode()
        {
            return new Crc(8, 0x1D, 0xFD, false, false, 0x00, 0x7E);
        }

        public static Crc CreateCrc8Itu()
        {
            return new Crc(8, 0x07, 0x00, false, false, 0x55, 0xA1);
        }

        public static Crc CreateCrc8Maxim()
        {
            return new Crc(8, 0x31, 0x00, true, true, 0x00, 0xA1);
        }

        public static Crc CreateCrc8Rohc()
        {
            return new Crc(8, 0x07, 0xFF, true, true, 0x00, 0xD0);
        }

        public static Crc CreateCrc8Wcdma()
        {
            return new Crc(8, 0x9B, 0x00, true, true, 0x00, 0x25);
        }

        public static Crc CreateCrc16CcittFalse()
        {
            return new Crc(16, 0x1021, 0xFFFF, false, false, 0x0000, 0x29B1);
        }

        public static Crc CreateCrc16Arc()
        {
            return new Crc(16, 0x8005, 0x0000, true, true, 0x0000, 0xBB3D);
        }

        public static Crc CreateCrc16AugCcitt()
        {
            return new Crc(16, 0x1021, 0x1D0F, false, false, 0x0000, 0xE5CC);
        }

        public static Crc CreateCrc16Buypass()
        {
            return new Crc(16, 0x8005, 0x0000, false, false, 0x0000, 0xFEE8);
        }

        public static Crc CreateCrc16Cdma2000()
        {
            return new Crc(16, 0xC867, 0xFFFF, false, false, 0x0000, 0x4C06);
        }

        public static Crc CreateCrc16Dds110()
        {
            return new Crc(16, 0x8005, 0x800D, false, false, 0x0000, 0x9ECF);
        }

        public static Crc CreateCrc16DectR()
        {
            return new Crc(16, 0x589, 0x0000, false, false, 0x0001, 0x7E);
        }

        public static Crc CreateCrc16DectX()
        {
            return new Crc(16, 0x0589, 0x0000, false, false, 0x0000, 0x7F);
        }

        public static Crc CreateCrc16Dnp()
        {
            return new Crc(16, 0x3D65, 0x0000, true, true, 0xFFFF, 0xEA82);
        }

        public static Crc CreateCrc16En13757()
        {
            return new Crc(16, 0x3D65, 0x0000, false, false, 0xFFFF, 0xC2B7);
        }

        public static Crc CreateCrc16Genibus()
        {
            return new Crc(16, 0x1021, 0xFFFF, false, false, 0xFFFF, 0xD64E);
        }

        public static Crc CreateCrc16Maxim()
        {
            return new Crc(16, 0x8005, 0x0000, true, true, 0xFFFF, 0x44C2);
        }

        public static Crc CreateCrc16Mcrf4Xx()
        {
            return new Crc(16, 0x1021, 0xFFFF, true, true, 0x0000, 0x6F91);
        }

        public static Crc CreateCrc16Riello()
        {
            return new Crc(16, 0x1021, 0xB2AA, true, true, 0x0000, 0x63D0);
        }

        public static Crc CreateCrc16T10Dif()
        {
            return new Crc(16, 0x8BB7, 0x0000, false, false, 0x0000, 0xD0DB);
        }

        public static Crc CreateCrc16Teledisk()
        {
            return new Crc(16, 0xA097, 0x0000, false, false, 0x0000, 0xFB3);
        }

        public static Crc CreateCrc16Tms37157()
        {
            return new Crc(16, 0x1021, 0x89EC, true, true, 0x0000, 0x26B1);
        }

        public static Crc CreateCrc16Usb()
        {
            return new Crc(16, 0x8005, 0xFFFF, true, true, 0xFFFF, 0xB4C8);
        }

        public static Crc CreateCrcA()
        {
            return new Crc(16, 0x1021, 0xC6C6, true, true, 0x0000, 0xBF05);
        }

        public static Crc CreateCrc16Kermit()
        {
            return new Crc(16, 0x1021, 0x0000, true, true, 0x0000, 0x2189);
        }

        public static Crc CreateCrc16Modbus()
        {
            return new Crc(16, 0x8005, 0xFFFF, true, true, 0x0000, 0x4B37);
        }

        public static Crc CreateCrc16X25()
        {
            return new Crc(16, 0x1021, 0xFFFF, true, true, 0xFFFF, 0x906E);
        }

        public static Crc CreateCrc16Xmodem()
        {
            return new Crc(16, 0x1021, 0x0000, false, false, 0x0000, 0x31C3);
        }

        public static Crc CreateCrc24()
        {
            return new Crc(24, 0x864CFB, 0xB704CE, false, false, 0x000000, 0x21CF02);
        }

        public static Crc CreateCrc24FlexrayA()
        {
            return new Crc(24, 0x5D6DCB, 0xFEDCBA, false, false, 0x000000, 0x7979BD);
        }

        public static Crc CreateCrc24FlexrayB()
        {
            return new Crc(24, 0x5D6DCB, 0xABCDEF, false, false, 0x000000, 0x1F23B8);
        }

        public static Crc CreateCrc32()
        {
            return new Crc(32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xCBF43926);
        }

        public static Crc CreateCrc32Bzip2()
        {
            return new Crc(32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0xFFFFFFFF, 0xFC891918);
        }

        public static Crc CreateCrc32C()
        {
            return new Crc(32, 0x1EDC6F41, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xE3069283);
        }

        public static Crc CreateCrc32D()
        {
            return new Crc(32, 0xA833982B, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0x87315576);
        }

        public static Crc CreateCrc32Jamcrc()
        {
            return new Crc(32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0x00000000, 0x340BC6D9);
        }

        public static Crc CreateCrc32Mpeg2()
        {
            return new Crc(32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7);
        }

        public static Crc CreateCrc32Posix()
        {
            return new Crc(32, 0x04C11DB7, 0x00000000, false, false, 0xFFFFFFFF, 0x765E7680);
        }

        public static Crc CreateCrc32Q()
        {
            return new Crc(32, 0x814141AB, 0x00000000, false, false, 0x00000000, 0x3010BF7F);
        }

        public static Crc CreateCrc32Xfer()
        {
            return new Crc(32, 0x000000AF, 0x00000000, false, false, 0x00000000, 0xBD0BE338);
        }

        public static Crc CreateCrc40Gsm()
        {
            return new Crc(40, 0x0004820009, 0x0000000000, false, false, 0xFFFFFFFFFF, 0xD4164FC646);
        }

        public static Crc CreateCrc64()
        {
            return new Crc(64, 0x42F0E1EBA9EA3693, 0x0000000000000000, false, false, 0x0000000000000000, 0x6C40DF5F0B497347);
        }

        public static Crc CreateCrc64We()
        {
            return new Crc(64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, false, false, 0xFFFFFFFFFFFFFFFF, 0x62EC59E3F1A4F00A);
        }

        public static Crc CreateCrc64Xz()
        {
            return new Crc(64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, true, true, 0xFFFFFFFFFFFFFFFF, 0x995DC9BBDF1939FA);
        }
    }
}
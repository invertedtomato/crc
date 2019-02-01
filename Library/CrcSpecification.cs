namespace InvertedTomato.Checksum
{
    public static class CrcSpecification
    {

        public static Crc Crc8
        {
            get
            {
                if (null == _Crc8)
                {

                    _Crc8 = new Crc(8, 0x7, 0x00, false, false, 0x00, 0xF4);
                }
                return _Crc8;
            }
        }
        private static Crc _Crc8;

        public static Crc Crc8Cdma2000
        {
            get
            {
                if (null == _Crc8Cdma2000)
                {
                    _Crc8Cdma2000 = new Crc(8, 0x9B, 0xFF, false, false, 0x00, 0xDA);
                }
                return _Crc8Cdma2000;
            }
        }
        private static Crc _Crc8Cdma2000;

        public static Crc Crc8Darc
        {
            get
            {
                if (null == _Crc8Darc)
                {
                    _Crc8Darc = new Crc(8, 0x39, 0x00, true, true, 0x00, 0x15);
                }
                return _Crc8Darc;
            }
        }
        private static Crc _Crc8Darc;

        public static Crc Crc8DvbS2
        {
            get
            {
                if (null == _Crc8DvbS2)
                {
                    _Crc8DvbS2 = new Crc(8, 0xD5, 0x00, false, false, 0x00, 0xBC);
                }
                return _Crc8DvbS2;
            }
        }
        private static Crc _Crc8DvbS2;

        public static Crc Crc8Ebu
        {
            get
            {
                if (null == _Crc8Ebu)
                {
                    _Crc8Ebu = new Crc(8, 0x1D, 0xFF, true, true, 0x00, 0x97);
                }
                return _Crc8Ebu;
            }
        }
        private static Crc _Crc8Ebu;

        public static Crc Crc8ICode
        {
            get
            {
                if (null == _Crc8ICode)
                {
                    _Crc8ICode = new Crc(8, 0x1D, 0xFD, false, false, 0x00, 0x7E);
                }
                return _Crc8ICode;
            }
        }
        private static Crc _Crc8ICode;

        public static Crc Crc8Itu
        {
            get
            {
                if (null == _Crc8Itu)
                {
                    _Crc8Itu = new Crc(8, 0x07, 0x00, false, false, 0x55, 0xA1);
                }
                return _Crc8Itu;
            }
        }
        private static Crc _Crc8Itu;

        public static Crc Crc8Maxim
        {
            get
            {
                if (null == _Crc8Maxim)
                {
                    _Crc8Maxim = new Crc(8, 0x31, 0x00, true, true, 0x00, 0xA1);
                }
                return _Crc8Maxim;
            }
        }
        private static Crc _Crc8Maxim;

        public static Crc Crc8Rohc
        {
            get
            {
                if (null == _Crc8Rohc)
                {
                    _Crc8Rohc = new Crc(8, 0x07, 0xFF, true, true, 0x00, 0xD0);
                }
                return _Crc8Rohc;
            }
        }
        private static Crc _Crc8Rohc;

        public static Crc Crc8Wcdma
        {
            get
            {
                if (null == _Crc8Wcdma)
                {
                    _Crc8Wcdma = new Crc(8, 0x9B, 0x00, true, true, 0x00, 0x25);
                }
                return _Crc8Wcdma;
            }
        }
        private static Crc _Crc8Wcdma;

        public static Crc Crc16CcittFalse
        {
            get
            {
                if (null == _Crc16CcittFalse)
                {

                    //CRC-16
                    _Crc16CcittFalse = new Crc(16, 0x1021, 0xFFFF, false, false, 0x0000, 0x29B1);
                }
                return _Crc16CcittFalse;
            }
        }
        private static Crc _Crc16CcittFalse;

        public static Crc Crc16Arc
        {
            get
            {
                if (null == _Crc16Arc)
                {
                    _Crc16Arc = new Crc(16, 0x8005, 0x0000, true, true, 0x0000, 0xBB3D);
                }
                return _Crc16Arc;
            }
        }
        private static Crc _Crc16Arc;

        public static Crc Crc16AugCcitt
        {
            get
            {
                if (null == _Crc16AugCcitt)
                {
                    _Crc16AugCcitt = new Crc(16, 0x1021, 0x1D0F, false, false, 0x0000, 0xE5CC);
                }
                return _Crc16AugCcitt;
            }
        }
        private static Crc _Crc16AugCcitt;

        public static Crc Crc16Buypass
        {
            get
            {
                if (null == _Crc16Buypass)
                {
                    _Crc16Buypass = new Crc(16, 0x8005, 0x0000, false, false, 0x0000, 0xFEE8);
                }
                return _Crc16Buypass;
            }
        }
        private static Crc _Crc16Buypass;

        public static Crc Crc16Cdma2000
        {
            get
            {
                if (null == _Crc16Cdma2000)
                {
                    _Crc16Cdma2000 = new Crc(16, 0xC867, 0xFFFF, false, false, 0x0000, 0x4C06);
                }
                return _Crc16Cdma2000;
            }
        }
        private static Crc _Crc16Cdma2000;

        public static Crc Crc16Dds110
        {
            get
            {
                if (null == _Crc16Dds110)
                {
                    _Crc16Dds110 = new Crc(16, 0x8005, 0x800D, false, false, 0x0000, 0x9ECF);
                }
                return _Crc16Dds110;
            }
        }
        private static Crc _Crc16Dds110;

        public static Crc Crc16DectR
        {
            get
            {
                if (null == _Crc16DectR)
                {
                    _Crc16DectR = new Crc(16, 0x589, 0x0000, false, false, 0x0001, 0x7E);
                }
                return _Crc16DectR;
            }
        }
        private static Crc _Crc16DectR;

        public static Crc Crc16DectX
        {
            get
            {
                if (null == _Crc16DectX)
                {
                    _Crc16DectX = new Crc(16, 0x0589, 0x0000, false, false, 0x0000, 0x7F);
                }
                return _Crc16DectX;
            }
        }
        private static Crc _Crc16DectX;

        public static Crc Crc16Dnp
        {
            get
            {
                if (null == _Crc16Dnp)
                {
                    _Crc16Dnp = new Crc(16, 0x3D65, 0x0000, true, true, 0xFFFF, 0xEA82);
                }
                return _Crc16Dnp;
            }
        }
        private static Crc _Crc16Dnp;

        public static Crc Crc16En13757
        {
            get
            {
                if (null == _Crc16En13757)
                {
                    _Crc16En13757 = new Crc(16, 0x3D65, 0x0000, false, false, 0xFFFF, 0xC2B7);
                }
                return _Crc16En13757;
            }
        }
        private static Crc _Crc16En13757;

        public static Crc Crc16Genibus
        {
            get
            {
                if (null == _Crc16Genibus)
                {
                    _Crc16Genibus = new Crc(16, 0x1021, 0xFFFF, false, false, 0xFFFF, 0xD64E);
                }
                return _Crc16Genibus;
            }
        }
        private static Crc _Crc16Genibus;

        public static Crc Crc16Maxim
        {
            get
            {
                if (null == _Crc16Maxim)
                {
                    _Crc16Maxim = new Crc(16, 0x8005, 0x0000, true, true, 0xFFFF, 0x44C2);
                }
                return _Crc16Maxim;
            }
        }
        private static Crc _Crc16Maxim;

        public static Crc Crc16Mcrf4Xx
        {
            get
            {
                if (null == _Crc16Mcrf4Xx)
                {
                    _Crc16Mcrf4Xx = new Crc(16, 0x1021, 0xFFFF, true, true, 0x0000, 0x6F91);
                }
                return _Crc16Mcrf4Xx;
            }
        }
        private static Crc _Crc16Mcrf4Xx;

        public static Crc Crc16Riello
        {
            get
            {
                if (null == _Crc16Riello)
                {
                    _Crc16Riello = new Crc(16, 0x1021, 0xB2AA, true, true, 0x0000, 0x63D0);
                }
                return _Crc16Riello;
            }
        }
        private static Crc _Crc16Riello;

        public static Crc Crc16T10Dif
        {
            get
            {
                if (null == _Crc16T10Dif)
                {
                    _Crc16T10Dif = new Crc(16, 0x8BB7, 0x0000, false, false, 0x0000, 0xD0DB);
                }
                return _Crc16T10Dif;
            }
        }
        private static Crc _Crc16T10Dif;

        public static Crc Crc16Teledisk
        {
            get
            {
                if (null == _Crc16Teledisk)
                {
                    _Crc16Teledisk = new Crc(16, 0xA097, 0x0000, false, false, 0x0000, 0xFB3);
                }
                return _Crc16Teledisk;
            }
        }
        private static Crc _Crc16Teledisk;

        public static Crc Crc16Tms37157
        {
            get
            {
                if (null == _Crc16Tms37157)
                {
                    _Crc16Tms37157 = new Crc(16, 0x1021, 0x89EC, true, true, 0x0000, 0x26B1);
                }
                return _Crc16Tms37157;
            }
        }
        private static Crc _Crc16Tms37157;

        public static Crc Crc16Usb
        {
            get
            {
                if (null == _Crc16Usb)
                {
                    _Crc16Usb = new Crc(16, 0x8005, 0xFFFF, true, true, 0xFFFF, 0xB4C8);
                }
                return _Crc16Usb;
            }
        }
        private static Crc _Crc16Usb;

        public static Crc CrcA
        {
            get
            {
                if (null == _CrcA)
                {
                    _CrcA = new Crc(16, 0x1021, 0xC6C6, true, true, 0x0000, 0xBF05);
                }
                return _CrcA;
            }
        }
        private static Crc _CrcA;

        public static Crc Crc16Kermit
        {
            get
            {
                if (null == _Crc16Kermit)
                {
                    _Crc16Kermit = new Crc(16, 0x1021, 0x0000, true, true, 0x0000, 0x2189);
                }
                return _Crc32;
            }
        }
        private static Crc _Crc16Kermit;

        public static Crc Crc16Modbus
        {
            get
            {
                if (null == _Crc16Modbus)
                {
                    _Crc16Modbus = new Crc(16, 0x8005, 0xFFFF, true, true, 0x0000, 0x4B37);
                }
                return _Crc16Modbus;
            }
        }
        private static Crc _Crc16Modbus;

        public static Crc Crc16X25
        {
            get
            {
                if (null == _Crc16X25)
                {
                    _Crc16X25 = new Crc(16, 0x1021, 0xFFFF, true, true, 0xFFFF, 0x906E);
                }
                return _Crc16X25;
            }
        }
        private static Crc _Crc16X25;

        public static Crc Crc16Xmodem
        {
            get
            {
                if (null == _Crc16Xmodem)
                {
                    _Crc16Xmodem = new Crc(16, 0x1021, 0x0000, false, false, 0x0000, 0x31C3);
                }
                return _Crc16Xmodem;
            }
        }
        private static Crc _Crc16Xmodem;

        public static Crc Crc24
        {
            get
            {
                if (null == _Crc24)
                {
                    _Crc24 = new Crc(24, 0x864CFB, 0xB704CE, false, false, 0x000000, 0x21CF02);
                }
                return _Crc24;
            }
        }
        private static Crc _Crc24;

        public static Crc Crc24FlexrayA
        {
            get
            {
                if (null == _Crc24FlexrayA)
                {
                    _Crc24FlexrayA = new Crc(24, 0x5D6DCB, 0xFEDCBA, false, false, 0x000000, 0x7979BD);
                }
                return _Crc24FlexrayA;
            }
        }
        private static Crc _Crc24FlexrayA;

        public static Crc Crc24FlexrayB
        {
            get
            {
                if (null == _Crc24FlexrayB)
                {
                    _Crc24FlexrayB = new Crc(24, 0x5D6DCB, 0xABCDEF, false, false, 0x000000, 0x1F23B8);
                }
                return _Crc24FlexrayB;
            }
        }
        private static Crc _Crc24FlexrayB;

        public static Crc Crc32
        {
            get
            {
                if (null == _Crc32)
                {
                    _Crc32 = new Crc(32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xCBF43926);
                }
                return _Crc32;
            }
        }
        private static Crc _Crc32;

        public static Crc Crc32Bzip2
        {
            get
            {
                if (null == _Crc32Bzip2)
                {
                    _Crc32Bzip2 = new Crc(32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0xFFFFFFFF, 0xFC891918);
                }
                return _Crc32Bzip2;
            }
        }
        private static Crc _Crc32Bzip2;

        public static Crc Crc32C
        {
            get
            {
                if (null == _Crc32C)
                {
                    _Crc32C = new Crc(32, 0x1EDC6F41, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xE3069283);
                }
                return _Crc32C;
            }
        }
        private static Crc _Crc32C;

        public static Crc Crc32D
        {
            get
            {
                if (null == _Crc32D)
                {
                    _Crc32D = new Crc(32, 0xA833982B, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0x87315576);
                }
                return _Crc32D;
            }
        }
        private static Crc _Crc32D;

        public static Crc Crc32Jamcrc
        {
            get
            {
                if (null == _Crc32Jamcrc)
                {
                    _Crc32Jamcrc = new Crc(32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0x00000000, 0x340BC6D9);
                }
                return _Crc32Jamcrc;
            }
        }
        private static Crc _Crc32Jamcrc;

        public static Crc Crc32Mpeg2
        {
            get
            {
                if (null == _Crc32Mpeg2)
                {
                    _Crc32Mpeg2 = new Crc(32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7);
                }
                return _Crc32Mpeg2;
            }
        }
        private static Crc _Crc32Mpeg2;

        public static Crc Crc32Posix
        {
            get
            {
                if (null == _Crc32Posix)
                {
                    _Crc32Posix = new Crc(32, 0x04C11DB7, 0x00000000, false, false, 0xFFFFFFFF, 0x765E7680);
                }
                return _Crc32Posix;
            }
        }
        private static Crc _Crc32Posix;

        public static Crc Crc32Q
        {
            get
            {
                if (null == _Crc32Q)
                {
                    _Crc32Q = new Crc(32, 0x814141AB, 0x00000000, false, false, 0x00000000, 0x3010BF7F);
                }
                return _Crc32Q;
            }
        }
        private static Crc _Crc32Q;

        public static Crc Crc32Xfer
        {
            get
            {
                if (null == _Crc32Xfer)
                {
                    _Crc32Xfer = new Crc(32, 0x000000AF, 0x00000000, false, false, 0x00000000, 0xBD0BE338);
                }
                return _Crc32Xfer;
            }
        }
        private static Crc _Crc32Xfer;

        public static Crc Crc40Gsm
        {
            get
            {
                if (null == _Crc40Gsm)
                {
                    _Crc40Gsm = new Crc(40, 0x4820009, 0x0000, false, false, 0xFFFFFFFFFF, 0xD4164FC646);
                }
                return _Crc40Gsm;
            }
        }
        private static Crc _Crc40Gsm;

        public static Crc Crc64
        {
            get
            {
                if (null == _Crc64)
                {

                    //CRC-64
                    _Crc64 = new Crc(64, 0x42F0E1EBA9EA3693, 0x00000000, false, false, 0x00000000, 0x6C40DF5F0B497347);
                }
                return _Crc64;
            }
        }
        private static Crc _Crc64;

        public static Crc Crc64We
        {
            get
            {
                if (null == _Crc64We)
                {
                    _Crc64We = new Crc(64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, false, false, 0xFFFFFFFFFFFFFFFF, 0x62EC59E3F1A4F00A);
                }
                return _Crc64We;
            }
        }
        private static Crc _Crc64We;

        public static Crc Crc64Xz
        {
            get
            {
                if (null == _Crc64Xz)
                {
                    _Crc64Xz = new Crc(64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, true, true, 0xFFFFFFFFFFFFFFFF, 0x995DC9BBDF1939FA);
                }
                return _Crc64Xz;
            }
        }
        private static Crc _Crc64Xz;
    }
}
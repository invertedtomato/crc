# InvertedTomato.CRC
## TLDR; How do I make it go?
```c#
// Create a new instance of Crc using the algorithm of your choice
var crc = new CrcAlgorithm.CreateCrc16CcittFalse();

// Give it some bytes to chew on - you can call this multiple times if needed
crc.Append(Encoding.ASCII.GetBytes("Hurray for cake!"));

// Get the output - as a hex string, byte array or unsigned integer
Console.WriteLine(crc.ToHexString());
```

## Introduction
CRC is an interesting standard. It's an interesting standard, because it isn't really a standard at all. It seems rather better to be considered an idea. Many people have taken 
that core idea of CRC and implemented it in many different ways. At the time of writing [Wikipedia lists 61 different implementations](https://en.wikipedia.org/wiki/Cyclic_redundancy_check). There are even tools available that support [over 100 implementations](http://reveng.sourceforge.net/).

Each of these implementations (or algorithms, as they're more commonly known) differ in the following ways:

 * They vary in the _width_, or the size of the outputted hash.
 * The use different underlying _polynomials_.
 * The algorithms are seeded with differing _initial values_.
 * Some _reflect the input_ before processing it
 * Some _reflect the output_ before returning it
 * Some apply a _XOR_ to the output return it

(This is taken straight from the [fantastic work of Ross N. Williams](https://raw.githubusercontent.com/invertedtomato/crc/master/Reference/crc_v3.txt) and I take no credit.)

InvertedTomato.Crc takes into account all of these parameters allowing it to be customised to the particular algorithm you need.

## Supported algorithms
InvertedTomato.Crc allows you to plug any parameters you'd like into the constructor to support any algorithm you'd like (so long as it's width is 8-64bits), however I've included some of the most common algorithms for convenience [borrowed from Meetantony](https://github.com/meetanthony/crccsharp/blob/master/CrcStdParams.cs).

Algorithm | Generator
======================
CRC-8 | CrcAlgorithm.CreateCrc8()
CRC-8/CDMA2000 | CrcAlgorithm.CreateCrc8Cdma2000()
CRC-8/DARC | CrcAlgorithm.CreateCrc8Darc()
CRC-8/DVB-S2 | CrcAlgorithm.CreateCrc8DvbS2()
CRC-8/EBU | CrcAlgorithm.CreateCrc8Ebu()
CRC-8/I-CODE | CrcAlgorithm.CreateCrc8ICode()
CRC-8/ITU | CrcAlgorithm.CreateCrc8Itu()
CRC-8/MAXIM | CrcAlgorithm.CreateCrc8Maxim()
CRC-8/ROHC | CrcAlgorithm.CreateCrc8Rohc()
CRC-8/WCDMA | CrcAlgorithm.CreateCrc8Wcdma()
CRC-16/CCITT-FALSE | CrcAlgorithm.CreateCrc16CcittFalse()
CRC-16/ARC | CrcAlgorithm.CreateCrc16Arc()
CRC-16/AUG-CCITT | CrcAlgorithm.CreateCrc16AugCcitt()
CRC-16/BUYPASS | CrcAlgorithm.CreateCrc16Buypass()
CRC-16/CDMA2000 | CrcAlgorithm.CreateCrc16Cdma2000()
CRC-16/DDS-110 | CrcAlgorithm.CreateCrc16Dds110()
CRC-16/DECT-R | CrcAlgorithm.CreateCrc16DectR()
CRC-16/DECT-X | CrcAlgorithm.CreateCrc16DectX()
CRC-16/DNP | CrcAlgorithm.CreateCrc16Dnp()
CRC-16/EN-13757 | CrcAlgorithm.CreateCrc16En13757()
CRC-16/GENIBUS | CrcAlgorithm.CreateCrc16Genibus()
CRC-16/MAXIM | CrcAlgorithm.CreateCrc16Maxim()
CRC-16/MCRF4XX | CrcAlgorithm.CreateCrc16Mcrf4Xx()
CRC-16/RIELLO | CrcAlgorithm.CreateCrc16Riello()
CRC-16/T10-DIF | CrcAlgorithm.CreateCrc16T10Dif()
CRC-16/TELEDISK | CrcAlgorithm.CreateCrc16Teledisk()
CRC-16/TMS37157 | CrcAlgorithm.CreateCrc16Tms37157()
CRC-16/USB | CrcAlgorithm.CreateCrc16Usb()
CRC-A | CrcAlgorithm.CreateCrcA()
CRC-16/KERMIT | CrcAlgorithm.CreateCrc16Kermit()
CRC-16/MODBUS | CrcAlgorithm.CreateCrc16Modbus()
CRC-16/X-25 | CrcAlgorithm.CreateCrc16X25()
CRC-16/XMODEM | CrcAlgorithm.CreateCrc16Xmodem()
CRC-24 | CrcAlgorithm.CreateCrc24()
CRC-24/FLEXRAY-A | CrcAlgorithm.CreateCrc24FlexrayA()
CRC-24/FLEXRAY-B | CrcAlgorithm.CreateCrc24FlexrayB()
CRC-32 | CrcAlgorithm.CreateCrc32()
CRC-32/BZIP2 | CrcAlgorithm.CreateCrc32Bzip2()
CRC-32C | CrcAlgorithm.CreateCrc32C()
CRC-32D | CrcAlgorithm.CreateCrc32D()
CRC-32/JAMCRC | CrcAlgorithm.CreateCrc32Jamcrc()
CRC-32/MPEG-2 | CrcAlgorithm.CreateCrc32Mpeg2()
CRC-32/POSIX | CrcAlgorithm.CreateCrc32Posix()
CRC-32Q | CrcAlgorithm.CreateCrc32Q()
CRC-32/XFER | CrcAlgorithm.CreateCrc32Xfer()
CRC-40/GSM | CrcAlgorithm.CreateCrc40Gsm()
CRC-64 | CrcAlgorithm.CreateCrc64()
CRC-64/WE | CrcAlgorithm.CreateCrc64We()
CRC-64/XZ | CrcAlgorithm.CreateCrc64Xz()

## Further reading
If you're keen to know more about CRC, I can't over-recommend [Ross N. Williams's 'A Painless Guide ot CRC Error Detection Algorithms](https://raw.githubusercontent.com/invertedtomato/crc/master/Reference/crc_v3.txt). I haven't been able to find the original URL of it though, so I've mirrored it here.
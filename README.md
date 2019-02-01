# InvertedTomato.CRC
## TLDR; How do I make it go?
```c#
// Create a new instance of Crc using the algorithm of your choice - there's a stack included, but you can also roll your own
var crc = new CrcAlgorithm.CreateCrc16CcittFalse();

// Give it some bytes to chew on - you can call this multiple times if that works for you
crc.Append(Encoding.ASCII.GetBytes("Hurray for cake!"));

// Get the output - as a hex string, byte array or unsigned integer
Console.WriteLine(crc.ToHexString());
```

## Introduction
CRC is an interesting standard. It's an interesting standard, because it isn't really a standard at all. It seems rather better to be considered an idea. Many people have taken 
that core idea of CRC and implemented it in many different ways. At the time of writing [Wikipedia lists 61 different implementations](https://en.wikipedia.org/wiki/Cyclic_redundancy_check). There are tools available that support [over 100 implementations](http://reveng.sourceforge.net/).

Each of these implementations (or algorithms, as they're more commonly known) differ in the following ways:

    *Width:* This is the width of the algorithm expressed in bits.
    *Polynomial:* 
    *Initial value:* The initial value of the register when the algorithm starts. 
    *If the input is reflected:* If not the input bytes are processed with bit 7 being treated as the most significant bit (MSB) and bit 0 being treated as the least significant bit. Otherwise each byte is reflected before being processed.
    *If the output is reflected:* If not the final value in the register is fed into the XOROUT stage directly, otherwise the final register value is reflected first.
    *Output XOR:* An XOR applied to the final register value (after the output reflection stage) before the value is returned as the official checksum.

(This is taken straight from the [fantastic work of Ross N. Williams](https://raw.githubusercontent.com/invertedtomato/crc/master/Reference/crc_v3.txt) and I take no credit.)

InvertedTomato.Crc takes into account all of these parameters allowing it to be customised to the particular algorithm you need.
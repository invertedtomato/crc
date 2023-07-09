using System;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedType.Global
// ReSharper disable NotAccessedField.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace InvertedTomato.Crc;

// TODO: consider endianness
/// <summary>
///     Library for computing CRCs of any algorithm in sizes of 8-64bits.
/// </summary>
/// <remarks>
///     Based loosely on https://github.com/meetanthony/crccsharp and drawing from the fantastic work from R. Williams
///     http://www.ross.net/crc/download/crc_v3.txt
/// </remarks>
public class CrcAlgorithm
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
    public readonly UInt64 Check;

    /// <summary>
    ///     The initial value of the register when the algorithm starts.
    /// </summary>
    public readonly UInt64 Initial;

    /// <summary>
    ///     If the input is to be reflected before processing.
    /// </summary>
    /// <remarks>
    ///     If it is FALSE, input bytes are processed with bit 7 being treated as the most significant bit (MSB) and bit 0
    ///     being treated as the least significant bit. If this parameter is TRUE, each byte is reflected before being
    ///     processed.
    /// </remarks>
    public readonly Boolean IsInputReflected;

    /// <summary>
    ///     Is the output to be reflected.
    /// </summary>
    /// <remarks>
    ///     If it is set to FALSE, the final value in the register is fed into the OutputXor stage directly, otherwise, if
    ///     this parameter is TRUE, the final register value is reflected first.
    /// </remarks>
    public readonly Boolean IsOutputReflected;

    /// <summary>
    ///     Mask used internally to hide unwanted data in the 64bit working registers.
    /// </summary>
    private readonly UInt64 _mask;

    /// <summary>
    ///     Name given to the algorithm.
    /// </summary>
    public readonly String Name;

    /// <summary>
    ///     This value is XORed to the final register value (after the IsOutputReflected stage) before the value is
    ///     returned as the official checksum.
    /// </summary>
    public readonly UInt64 OutputXor;

    /// <summary>
    ///     The polynomial used for the CRC calculation, omitting the top bit.
    /// </summary>
    /// <remarks>
    ///     The top bit of the poly should be omitted. For example, if the poly is 10110, you should specify 0x06. Also,
    ///     an important aspect of this parameter is that it represents the unreflected poly; the bottom bit of this parameter
    ///     is always the LSB of the divisor during the division regardless of whether the algorithm being modelled is
    ///     reflected.
    /// </remarks>
    public readonly UInt64 Polynomial;

    /// <summary>
    ///     Lookup table that is populated at construction time to facilitate fastest possible computation.
    /// </summary>
    private readonly UInt64[] _preComputationTable = new UInt64[256];

    private readonly Int32 _toRight;

    /// <summary>
    ///     Width of the algorithm expressed in bits.
    /// </summary>
    /// <remarks>
    ///     This is one less bit than the width of the Polynomial.
    /// </remarks>
    public readonly Int32 Width;

    /// <summary>
    ///     Accumulated CRC-32C of all buffers processed so far.
    /// </summary>
    private UInt64 _current;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CrcAlgorithm"/> class.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="width"/> must be a multiple of 8 and between 8 and 64.
    /// </exception>
    public CrcAlgorithm(String name, Int32 width, UInt64 polynomial, UInt64 initial, Boolean isInputReflected, Boolean isOutputReflected, UInt64 outputXor, UInt64 check = 0)
    {
        if (width < 8 || width > 64) throw new ArgumentOutOfRangeException(nameof(width), "Must be a multiple of 8 and between 8 and 64.");

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
        _mask = UInt64.MaxValue >> (64 - width);

        // Create lookup table
        for (var i = 0; i < _preComputationTable.Length; i++)
        {
            var r = (UInt64)i;
            if (IsInputReflected)
                r = ReverseBits(r, width);
            else if (width > 8) r <<= width - 8;

            var lastBit = 1ul << (width - 1);

            for (var j = 0; j < 8; j++)
                if ((r & lastBit) != 0)
                    r = (r << 1) ^ Polynomial;
                else
                    r <<= 1;

            if (IsInputReflected) r = ReverseBits(r, width);

            _preComputationTable[i] = r;
        }

        // Calculate non-reflected output adjustment
        if (!IsOutputReflected)
        {
            _toRight = Width - 8;
            _toRight = _toRight < 0 ? 0 : _toRight;
        }

        // Initialise the current value
        Clear();
    }

    /// <summary>
    ///     Compute the hash of a byte array. This can be called multiple times for consecutive blocks of input.
    /// </summary>
    public CrcAlgorithm Append(Byte[] input)
    {
        Append(input, 0, input.Length);
        return this;
    }

    /// <summary>
    ///     Compute the hash of a byte array with a defined offset and count. This can be called multiple times for
    ///     consecutive blocks of input.
    /// </summary>
    public CrcAlgorithm Append(Byte[] input, Int32 offset, Int32 count)
    {
        if (null == input) throw new ArgumentNullException(nameof(input));

        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

        if (count < 0 || offset + count > input.Length) throw new ArgumentOutOfRangeException(nameof(count));

        if (IsOutputReflected)
            for (var i = offset; i < offset + count; i++)
                _current = _preComputationTable[(_current ^ input[i]) & 0xFF] ^ (_current >> 8);
        else
            for (var i = offset; i < offset + count; i++)
                _current = _preComputationTable[((_current >> _toRight) ^ input[i]) & 0xFF] ^ (_current << 8);

        return this;
    }

    /// <summary>
    ///     Retrieve the CRC of the bytes that have been input so far.
    /// </summary>
    public UInt64 ToUInt64()
    {
        // Apply output XOR and mask unwanted bits
        return (_current ^ OutputXor) & _mask;
    }

    /// <summary>
    ///     Retrieve the CRC of the bytes that have been input so far.
    /// </summary>
    public Byte[] ToByteArray()
    {
        // TODO: this could be significantly optimised
        var output = ToUInt64();

        // Convert to byte array
        var result = BitConverter.GetBytes(output);

        // Correct for big-endian
        if (!BitConverter.IsLittleEndian) Array.Reverse(result);

        // Trim unwanted bytes
        Array.Resize(ref result, Width / 8);

        // Reverse bytes
        Array.Reverse(result);

        return result;
    }

    /// <summary>
    ///     Retrieve the CRC of the bytes that have been input so far.
    /// </summary>
    public String ToHexString() => ToByteArray().ToHexString();

    /// <summary>
    ///     Reset the state so that a new set of data can be input without being affected by previous sets.
    /// </summary>
    /// <remarks>
    ///     Typically this is called after retrieving a computed CRC (using ToByteArray() for example) and before calling
    ///     Append for a new computation run.
    /// </remarks>
    public void Clear()
    {
        // Initialise current
        _current = IsOutputReflected ? ReverseBits(Initial, Width) : Initial;
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

    // Common CRCs, based on https://crccalc.com/ and others
    public static CrcAlgorithm CreateCrc8() => new("CRC-8", 8, 0x07, 0x00, false, false, 0x00, 0xF4);
    public static CrcAlgorithm CreateCrc8Cdma2000() => new("CRC-8/CDMA2000", 8, 0x9B, 0xFF, false, false, 0x00, 0xDA);
    public static CrcAlgorithm CreateCrc8Darc() => new("CRC-8/DARC", 8, 0x39, 0x00, true, true, 0x00, 0x15);
    public static CrcAlgorithm CreateCrc8DvbS2() => new("CRC-8/DVB-S2", 8, 0xD5, 0x00, false, false, 0x00, 0xBC);
    public static CrcAlgorithm CreateCrc8Ebu() => new("CRC-8/EBU", 8, 0x1D, 0xFF, true, true, 0x00, 0x97);
    public static CrcAlgorithm CreateCrc8ICode() => new("CRC-8/I-CODE", 8, 0x1D, 0xFD, false, false, 0x00, 0x7E);
    public static CrcAlgorithm CreateCrc8Itu() => new("CRC-8/ITU", 8, 0x07, 0x00, false, false, 0x55, 0xA1);
    public static CrcAlgorithm CreateCrc8Maxim() => new("CRC-8/MAXIM", 8, 0x31, 0x00, true, true, 0x00, 0xA1);
    public static CrcAlgorithm CreateCrc8Rohc() => new("CRC-8/ROHC", 8, 0x07, 0xFF, true, true, 0x00, 0xD0);
    public static CrcAlgorithm CreateCrc8Wcdma() => new("CRC-8/WCDMA", 8, 0x9B, 0x00, true, true, 0x00, 0x25);
    public static CrcAlgorithm CreateCrc16Arc() => new("CRC-16/ARC", 16, 0x8005, 0x0000, true, true, 0x0000, 0xBB3D);
    public static CrcAlgorithm CreateCrc16AugCcitt() => new("CRC-16/AUG-CCITT", 16, 0x1021, 0x1D0F, false, false, 0x0000, 0xE5CC);
    public static CrcAlgorithm CreateCrc16Buypass() => new("CRC-16/BUYPASS", 16, 0x8005, 0x0000, false, false, 0x0000, 0xFEE8);
    public static CrcAlgorithm CreateCrc16CcittFalse() => new("CRC-16/CCITT-FALSE", 16, 0x1021, 0xFFFF, false, false, 0x0000, 0x29B1);
    public static CrcAlgorithm CreateCrc16Cdma2000() => new("CRC-16/CDMA2000", 16, 0xC867, 0xFFFF, false, false, 0x0000, 0x4C06);
    public static CrcAlgorithm CreateCrc16Dds110() => new("CRC-16/DDS-110", 16, 0x8005, 0x800D, false, false, 0x0000, 0x9ECF);
    public static CrcAlgorithm CreateCrc16DectR() => new("CRC-16/DECT-R", 16, 0x0589, 0x0000, false, false, 0x0001, 0x007E);
    public static CrcAlgorithm CreateCrc16DectX() => new("CRC-16/DECT-X", 16, 0x0589, 0x0000, false, false, 0x0000, 0x007F);
    public static CrcAlgorithm CreateCrc16Dnp() => new("CRC-16/DNP", 16, 0x3D65, 0x0000, true, true, 0xFFFF, 0xEA82);
    public static CrcAlgorithm CreateCrc16En13757() => new("CRC-16/EN-13757", 16, 0x3D65, 0x0000, false, false, 0xFFFF, 0xC2B7);
    public static CrcAlgorithm CreateCrc16Genibus() => new("CRC-16/GENIBUS", 16, 0x1021, 0xFFFF, false, false, 0xFFFF, 0xD64E);
    public static CrcAlgorithm CreateCrc16Kermit() => new("CRC-16/KERMIT", 16, 0x1021, 0x0000, true, true, 0x0000, 0x2189);
    public static CrcAlgorithm CreateCrc16Maxim() => new("CRC-16/MAXIM", 16, 0x8005, 0x0000, true, true, 0xFFFF, 0x44C2);
    public static CrcAlgorithm CreateCrc16Mcrf4Xx() => new("CRC-16/MCRF4XX", 16, 0x1021, 0xFFFF, true, true, 0x0000, 0x6F91);
    public static CrcAlgorithm CreateCrc16Modbus() => new("CRC-16/MODBUS", 16, 0x8005, 0xFFFF, true, true, 0x0000, 0x4B37);
    public static CrcAlgorithm CreateCrc16Riello() => new("CRC-16/RIELLO", 16, 0x1021, 0xB2AA, true, true, 0x0000, 0x63D0);
    public static CrcAlgorithm CreateCrc16T10Dif() => new("CRC-16/T10-DIF", 16, 0x8BB7, 0x0000, false, false, 0x0000, 0xD0DB);
    public static CrcAlgorithm CreateCrc16Teledisk() => new("CRC-16/TELEDISK", 16, 0xA097, 0x0000, false, false, 0x0000, 0xFB3);
    public static CrcAlgorithm CreateCrc16Tms37157() => new("CRC-16/TMS37157", 16, 0x1021, 0x89EC, true, true, 0x0000, 0x26B1);
    public static CrcAlgorithm CreateCrc16Usb() => new("CRC-16/USB", 16, 0x8005, 0xFFFF, true, true, 0xFFFF, 0xB4C8);
    public static CrcAlgorithm CreateCrc16X25() => new("CRC-16/X-25", 16, 0x1021, 0xFFFF, true, true, 0xFFFF, 0x906E);
    public static CrcAlgorithm CreateCrc16Xmodem() => new("CRC-16/XMODEM", 16, 0x1021, 0x0000, false, false, 0x0000, 0x31C3);
    public static CrcAlgorithm CreateCrcA() => new("CRC-A", 16, 0x1021, 0xC6C6, true, true, 0x0000, 0xBF05);
    public static CrcAlgorithm CreateCrc24() => new("CRC-24", 24, 0x864CFB, 0xB704CE, false, false, 0x000000, 0x21CF02);
    public static CrcAlgorithm CreateCrc24FlexrayA() => new("CRC-24/FLEXRAY-A", 24, 0x5D6DCB, 0xFEDCBA, false, false, 0x000000, 0x7979BD);
    public static CrcAlgorithm CreateCrc24FlexrayB() => new("CRC-24/FLEXRAY-B", 24, 0x5D6DCB, 0xABCDEF, false, false, 0x000000, 0x1F23B8);
    public static CrcAlgorithm CreateCrc32() => new("CRC-32", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xCBF43926);
    public static CrcAlgorithm CreateCrc32Bzip2() => new("CRC-32/BZIP2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0xFFFFFFFF, 0xFC891918);
    public static CrcAlgorithm CreateCrc32C() => new("CRC-32C", 32, 0x1EDC6F41, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xE3069283);
    public static CrcAlgorithm CreateCrc32D() => new("CRC-32D", 32, 0xA833982B, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0x87315576);
    public static CrcAlgorithm CreateCrc32Jamcrc() => new("CRC-32/JAMCRC", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0x00000000, 0x340BC6D9);
    public static CrcAlgorithm CreateCrc32Mpeg2() => new("CRC-32/MPEG-2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7);
    public static CrcAlgorithm CreateCrc32Posix() => new("CRC-32/POSIX", 32, 0x04C11DB7, 0x00000000, false, false, 0xFFFFFFFF, 0x765E7680);
    public static CrcAlgorithm CreateCrc32Q() => new("CRC-32Q", 32, 0x814141AB, 0x00000000, false, false, 0x00000000, 0x3010BF7F);
    public static CrcAlgorithm CreateCrc32Sata() => new("CRC-32/SATA", 32, 0x04C11DB7, 0x52325032, false, false, 0x00000000, 0xCF72AFE8);
    public static CrcAlgorithm CreateCrc32Xfer() => new("CRC-32/XFER", 32, 0x000000AF, 0x00000000, false, false, 0x00000000, 0xBD0BE338);
    public static CrcAlgorithm CreateCrc40Gsm() => new("CRC-40/GSM", 40, 0x4820009, 0x0000000000, false, false, 0xFFFFFFFFFF, 0xD4164FC646);
    public static CrcAlgorithm CreateCrc64() => new("CRC-64", 64, 0x42F0E1EBA9EA3693, 0x00000000, false, false, 0x00000000, 0x6C40DF5F0B497347);

    public static CrcAlgorithm CreateCrc64We() => new("CRC-64/WE", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, false, false, 0xFFFFFFFFFFFFFFFF,
        0x62EC59E3F1A4F00A);

    public static CrcAlgorithm CreateCrc64Xz() => new("CRC-64/XZ", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, true, true, 0xFFFFFFFFFFFFFFFF,
        0x995DC9BBDF1939FA);
}
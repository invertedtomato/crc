using System;

namespace InvertedTomato.Crc.Extensions;

public static class HexStringExtensions
{
    public static String ToHexString(this UInt16 target, Int32 minBytes = 0)
    {
        var output = target.ToString("X");
        output = new String('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
        return output;
    }

    public static String ToHexString(this UInt32 target, Int32 minBytes = 0)
    {
        var output = target.ToString("X");
        output = new String('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
        return output;
    }

    public static String ToHexString(this UInt64 target, Int32 minBytes = 0)
    {
        var output = target.ToString("X");
        output = new String('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
        return output;
    }

    public static String ToHexString(this Byte[] target, Int32 minBytes = 0)
    {
        var output = BitConverter.ToString(target).Replace("-", "");
        output = new String('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
        return output;
    }
}
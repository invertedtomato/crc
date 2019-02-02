using System;

namespace InvertedTomato.Checksum.Extensions
{
    public static class HexStringExtensions
    {
        public static string ToHexString(this ushort target, int minBytes = 0)
        {
            var output = target.ToString("X").Replace("-", "");
            output = new string('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
            return output;
        }

        public static string ToHexString(this uint target, int minBytes = 0)
        {
            var output = target.ToString("X").Replace("-", "");
            output = new string('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
            return output;
        }

        public static string ToHexString(this ulong target, int minBytes = 0)
        {
            var output = target.ToString("X").Replace("-", "");
            output = new string('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
            return output;
        }

        public static string ToHexString(this byte[] target, int minBytes = 0)
        {
            var output = BitConverter.ToString(target).Replace("-", "");
            output = new string('0', Math.Max(0, minBytes * 2 - output.Length)) + output;
            return output;
        }
    }
}
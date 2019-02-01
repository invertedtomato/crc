
using System;

namespace InvertedTomato.Checksum.Extensions
{
    public static class HexStringExtensions
    {
        public static String ToHexString(this UInt64 target)
        {
            return target.ToString("X").Replace("-", "");
        }

        public static String ToHexString(this Byte[] target){
            return BitConverter.ToString(target).Replace("-","");
        }
    }
}
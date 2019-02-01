
using System;
using Xunit;

namespace InvertedTomato.Checksum
{
    public static class Extensions
    {
        public static String ToHexString(this UInt64 target)
        {
            return target.ToString("X").Replace("-", "");
        }
    }
}
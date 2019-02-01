using System;
using Xunit;

namespace InvertedTomato.Checksum
{
    public class CrcTests
    {
        private const String CHECK_STRING= "123456789";

        [Fact]
        public void Crc8()
        {
            Assert.Equal(CrcSpecification.Crc8.Check, CrcSpecification.Crc8.Compute(CHECK_STRING));
        }

        [Fact]
        public void Crc16CcittFalse()
        {
            Assert.Equal(CrcSpecification.Crc16CcittFalse.Check, CrcSpecification.Crc16CcittFalse.Compute(CHECK_STRING));
        }

        [Fact]
        public void Crc32()
        {
            Assert.Equal(CrcSpecification.Crc32.Check, CrcSpecification.Crc32.Compute(CHECK_STRING));
        }
    }
}

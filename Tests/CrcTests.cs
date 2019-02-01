using System;
using Xunit;
using Xunit.Abstractions;

namespace InvertedTomato.Checksum
{
    public class CrcTests
    {
        private const String CHECK_STRING = "123456789";

        private readonly ITestOutputHelper Output;

        public CrcTests(ITestOutputHelper output)
        {
            Output = output;
        }

        [Fact]
        public void CrcSpecifications()
        {
            var type = typeof(CrcSpecification);
            foreach (var p in type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                var v = (Crc)p.GetValue(null); // static classes cannot be instanced, so use null...
                                               //do something with v

                Output.WriteLine($"Testing {p.Name}... ");
                Assert.Equal(v.Check.ToHexString(), v.Compute(CHECK_STRING).ToHexString());

            }
        }

        [Fact]
        public void Crc8()
        {
            Assert.Equal(CrcSpecification.Crc8.Check.ToHexString(), CrcSpecification.Crc8.Compute(CHECK_STRING).ToHexString());
        }

        [Fact]
        public void Crc16CcittFalse()
        {
            Assert.Equal(CrcSpecification.Crc16CcittFalse.Check.ToHexString(), CrcSpecification.Crc16CcittFalse.Compute(CHECK_STRING).ToHexString());
        }

        [Fact]
        public void Crc32()
        {
            Assert.Equal(CrcSpecification.Crc32.Check.ToHexString(), CrcSpecification.Crc32.Compute(CHECK_STRING).ToHexString());
        }
    }
}

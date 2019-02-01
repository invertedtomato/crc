using System;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using InvertedTomato.Checksum.Extensions;

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
        public void AllCrcAlgorithms()
        {
            // Convert check to byte array;
            var input = Encoding.ASCII.GetBytes(CHECK_STRING);

            // Loop through all known CRC specifications
            var type = typeof(CrcAlgorithm);
            foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                // Select specification for testing
                var crc = (Crc)method.Invoke(null, new Object[] { });
                Output.WriteLine($"Testing {crc.Name}... ");

                // Determine expected output
                var expected = crc.Check.ToHexString();
                expected = new String('0', crc.Width / 8 * 2 - expected.Length) + expected; // Pad with zero prefix

                // Run specification
                crc.Append(input);
                var output = crc.ToHexString();

                // Check output;
                Assert.Equal(expected, output);
            }
        }

        [Theory]
        [InlineData("123456789", 0x29B1)]
        [InlineData("cake", 0x0146)]
        [InlineData("rain", 0xBB5C)]
        [InlineData("", 0xFFFF)] // http://crccalc.com says the value should be 0xE1F0, however logic says it must be incorrect
        public void Crc16CittFalse(String input, UInt16 expected)
        {
            var crc = CrcAlgorithm.CreateCrc16CcittFalse();

            crc.Append(Encoding.ASCII.GetBytes(input));
            var output = crc.ToUInt64();

            Assert.Equal(expected, output);
        }

        [Theory]
        [InlineData("123456789", 0xCBF43926)]
        [InlineData("cake", 0xFA13015D)]
        [InlineData("rain", 0xB7528AAD)]
        [InlineData("", 0x00000000)] // http://crccalc.com says the value should be 0xD202EF8D, however logic says it must be incorrect
        public void Crc32(String input, UInt32 expected)
        {
            var crc = CrcAlgorithm.CreateCrc32();

            crc.Append(Encoding.ASCII.GetBytes(input));
            var output = crc.ToUInt64();

            Assert.Equal(expected, output);
        }
    }
}

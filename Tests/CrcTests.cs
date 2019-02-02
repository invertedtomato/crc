using System.Reflection;
using System.Text;
using InvertedTomato.Checksum.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace InvertedTomato.Checksum
{
    public class CrcTests
    {
        public CrcTests(ITestOutputHelper output)
        {
            Output = output;
        }

        private const string CHECK_STRING = "123456789";

        private readonly ITestOutputHelper Output;

        [Theory]
        [InlineData("123456789", 0x29B1)]
        [InlineData("cake", 0x0146)]
        [InlineData("rain", 0xBB5C)]
        [InlineData("",
            0xFFFF)] // http://crccalc.com says the value should be 0xE1F0, however logic says it must be incorrect
        public void Crc16CittFalse_Samples(string input, ushort expected)
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
        [InlineData("",
            0x00000000)] // http://crccalc.com says the value should be 0xD202EF8D, however logic says it must be incorrect
        public void Crc32_Samples(string input, uint expected)
        {
            var crc = CrcAlgorithm.CreateCrc32();

            crc.Append(Encoding.ASCII.GetBytes(input));
            var output = crc.ToUInt64();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void AllCrcAlgorithms()
        {
            // Convert check to byte array;
            var input = Encoding.ASCII.GetBytes(CHECK_STRING);

            // Loop through all known CRC specifications
            var type = typeof(CrcAlgorithm);
            foreach (var method in type.GetMethods(BindingFlags.Static |
                                                   BindingFlags.Public))
            {
                // Select specification for testing
                var crc = (Crc) method.Invoke(null, new object[] { });
                Output.WriteLine($"Testing {crc.Name}... ");

                // Determine expected output
                var expected = crc.Check.ToHexString();
                expected = new string('0', crc.Width / 8 * 2 - expected.Length) + expected; // Pad with zero prefix

                // Run specification
                crc.Append(input);
                var output = crc.ToHexString();

                // Check output;
                Assert.Equal(expected, output);
            }
        }

        [Fact]
        public void Crc16XModem()
        {
            string expected;
            string output;
            var crc = CrcAlgorithm.CreateCrc16Xmodem();

            // 0 bytes
            expected = Crc16Ccitt.ComputeInteger(new byte[] { }).ToHexString(2);
            output = crc.ToHexString();
            Assert.Equal(expected, output);

            // 1 byte complete
            for (byte i = 0; i < byte.MaxValue; i++)
            {
                // Compute expected
                expected = Crc16Ccitt.ComputeInteger(new[] {i}).ToHexString(2);

                // Compute actual
                crc.Append(i);
                output = crc.ToHexString();
                crc.Clear();

                // Compare
                Assert.Equal(expected, output);
            }

            // 2 byte complete
            for (byte i = 0; i < byte.MaxValue; i++)
            for (byte j = 0; j < byte.MaxValue; j++)
            {
                // Compute expected
                expected = Crc16Ccitt.ComputeInteger(new[] {i, j}).ToHexString(2);

                // Compute actual
                crc.Append(new[] {i, j});
                output = crc.ToHexString();
                crc.Clear();

                // Compare
                Assert.Equal(expected, output);
            }

            // 3-256 byte
            for (byte length = 0; length < byte.MaxValue; length++)
            for (byte b = 0; b < byte.MaxValue; b++)
            {
                var input = new byte[length];
                for (var i = 0; i < input.Length; i++) input[i] = b;

                // Compute expected
                expected = Crc16Ccitt.ComputeInteger(input).ToHexString(2);

                // Compute actual
                crc.Append(input);
                output = crc.ToHexString();
                crc.Clear();

                // Compare
                Assert.Equal(expected, output);
            }
        }
    }
}
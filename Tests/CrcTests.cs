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
        public void CrcSpecifications()
        {
            // Convert check to byte array;
            var input = Encoding.ASCII.GetBytes(CHECK_STRING);

            // Loop through all known CRC specifications
            var type = typeof(CrcSpecification);
            foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                // Select specification for testing
                var crc = (Crc)method.Invoke(null, new Object[] { });
                Output.WriteLine($"Testing {method.Name}... ");

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
    }
}

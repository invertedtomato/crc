using System;
using System.Text;
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
            // Convert check to byte array;
            var input = Encoding.ASCII.GetBytes(CHECK_STRING);

            // Loop through all known CRC specifications
            var type = typeof(CrcSpecification);
            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                var crc = (Crc)property.GetValue(null);

                // Execute check run on specification
                Output.WriteLine($"Testing {property.Name}... ");
                Assert.Equal(crc.Check.ToHexString(), crc.Compute(input).ToHexString().TrimStart('0')); // TODO: Remove TrimStart

            }
        }
    }
}

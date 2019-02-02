using System;
using System.Text;
using InvertedTomato.IO;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of Crc using the algorithm of your choice
            var crc = CrcAlgorithm.CreateCrc16CcittFalse();

            // Give it some bytes to chew on - you can call this multiple times if needed
            crc.Append(Encoding.ASCII.GetBytes("Hurray for cake!"));

            // Get the output - as a hex string, byte array or unsigned integer
            Console.WriteLine(crc.ToHexString());
        }
    }
}
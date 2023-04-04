using System;
using InvertedTomato.IO.Extensions;

namespace InvertedTomato.IO;

// TODO: consider endianness
/// <summary>
///     Library for computing CRCs of any algorithm in sizes of 8-64bits.
/// </summary>
/// <remarks>
///     Based loosely on https://github.com/meetanthony/crccsharp and drawing from the fantastic work from R. Williams
///     http://www.ross.net/crc/download/crc_v3.txt
/// </remarks>
public class Crc {
	/// <summary>
	///     The checksum obtained when the ASCII string "123456789" is fed through the specified algorithm (i.e.
	///     0x313233...).
	/// </summary>
	/// <remarks>
	///     This field is not strictly part of the definition, and, in the event of an inconsistency between this field
	///     and the other field, the other fields take precedence. This field is a check value that can be used as a weak
	///     validator of implementations of the algorithm.
	/// </remarks>
	public readonly UInt64 Check;

	/// <summary>
	///     The initial value of the register when the algorithm starts.
	/// </summary>
	public readonly UInt64 Initial;

	/// <summary>
	///     If the input is to be reflected before processing.
	/// </summary>
	/// <remarks>
	///     If it is FALSE, input bytes are processed with bit 7 being treated as the most significant bit (MSB) and bit 0
	///     being treated as the least significant bit. If this parameter is TRUE, each byte is reflected before being
	///     processed.
	/// </remarks>
	public readonly Boolean IsInputReflected;

	/// <summary>
	///     Is the output to be reflected.
	/// </summary>
	/// <remarks>
	///     If it is set to FALSE, the final value in the register is fed into the OutputXor stage directly, otherwise, if
	///     this parameter is TRUE, the final register value is reflected first.
	/// </remarks>
	public readonly Boolean IsOutputReflected;

	/// <summary>
	/// Mask used internally to hide unwanted data in the 64bit working registers.
	/// </summary>
	private readonly UInt64 Mask;

	/// <summary>
	///     Name given to the algorithm.
	/// </summary>
	public readonly String Name;

	/// <summary>
	///     This value is XORed to the final register value (after the IsOutputReflected stage) before the value is
	///     returned as the official checksum.
	/// </summary>
	public readonly UInt64 OutputXor;

	/// <summary>
	///     The polynomial used for the CRC calculation, omitting the top bit.
	/// </summary>
	/// <remarks>
	///     The top bit of the poly should be omitted. For example, if the poly is 10110, you should specify 0x06. Also,
	///     an important aspect of this parameter is that it represents the unreflected poly; the bottom bit of this parameter
	///     is always the LSB of the divisor during the division regardless of whether the algorithm being modelled is
	///     reflected.
	/// </remarks>
	public readonly UInt64 Polynomial;

	/// <summary>
	///     Lookup table that is populated at construction time to facilitate fastest possible computation.
	/// </summary>
	private readonly UInt64[] PrecomputationTable = new UInt64[256];

	private readonly Int32 ToRight;

	/// <summary>
	///     Width of the algorithm expressed in bits.
	/// </summary>
	/// <remarks>
	///     This is one less bit than the width of the Polynomial.
	/// </remarks>
	public readonly Int32 Width;

	/// <summary>
	///     Accumulated CRC-32C of all buffers processed so far.
	/// </summary>
	private UInt64 Current;


	public Crc(String name, Int32 width, UInt64 polynomial, UInt64 initial, Boolean isInputReflected, Boolean isOutputReflected, UInt64 outputXor, UInt64 check = 0) {
		if (width < 8 || width > 64) {
			throw new ArgumentOutOfRangeException(nameof(width), "Must be a multiple of 8 and between 8 and 64.");
		}

		// Store values
		Name = name;
		Width = width;
		Polynomial = polynomial;
		Initial = initial;
		IsInputReflected = isInputReflected;
		IsOutputReflected = isOutputReflected;
		OutputXor = outputXor;
		Check = check;

		// Compute mask
		Mask = UInt64.MaxValue >> (64 - width);

		// Create lookup table
		for (var i = 0; i < PrecomputationTable.Length; i++) {
			var r = (UInt64) i;
			if (IsInputReflected) {
				r = ReverseBits(r, width);
			} else if (width > 8) {
				r <<= width - 8;
			}

			var lastBit = 1ul << (width - 1);

			for (var j = 0; j < 8; j++) {
				if ((r & lastBit) != 0) {
					r = (r << 1) ^ Polynomial;
				} else {
					r <<= 1;
				}
			}

			if (IsInputReflected) {
				r = ReverseBits(r, width);
			}

			PrecomputationTable[i] = r;
		}

		// Calculate non-reflected output adjustment
		if (!IsOutputReflected) {
			ToRight = Width - 8;
			ToRight = ToRight < 0 ? 0 : ToRight;
		}

		// Initialise the current value
		Clear();
	}

	/// <summary>
	///     Compute the hash of a byte array. This can be called multiple times for consecutive blocks of input.
	/// </summary>
	public Crc Append(Byte[] input) {
		Append(input, 0, input.Length);
		return this;
	}

	/// <summary>
	///     Compute the hash of a byte array with a defined offset and count. This can be called multiple times for
	///     consecutive blocks of input.
	/// </summary>
	public Crc Append(Byte[] input, Int32 offset, Int32 count) {
		if (null == input) {
			throw new ArgumentNullException(nameof(input));
		}

		if (offset < 0) {
			throw new ArgumentOutOfRangeException(nameof(offset));
		}

		if (count < 0 || offset + count > input.Length) {
			throw new ArgumentOutOfRangeException(nameof(count));
		}

		if (IsOutputReflected) {
			for (var i = offset; i < offset + count; i++) {
				Current = PrecomputationTable[(Current ^ input[i]) & 0xFF] ^ (Current >> 8);
			}
		} else {
			for (var i = offset; i < offset + count; i++) {
				Current = PrecomputationTable[((Current >> ToRight) ^ input[i]) & 0xFF] ^ (Current << 8);
			}
		}

		return this;
	}

	/// <summary>
	///     Retrieve the CRC of the bytes that have been input so far.
	/// </summary>
	public UInt64 ToUInt64() {
		// Apply output XOR and mask unwanted bits
		return (Current ^ OutputXor) & Mask;
	}


	/// <summary>
	///     Retrieve the CRC of the bytes that have been input so far.
	/// </summary>
	public Byte[] ToByteArray() {
		// TODO: this could be significantly optimised
		var output = ToUInt64();

		// Convert to byte array
		var result = BitConverter.GetBytes(output);

		// Correct for big-endian 
		if (!BitConverter.IsLittleEndian) {
			Array.Reverse(result);
		}

		// Trim unwanted bytes
		Array.Resize(ref result, Width / 8);

		// Reverse bytes
		Array.Reverse(result);

		return result;
	}

	/// <summary>
	///     Retrieve the CRC of the bytes that have been input so far.
	/// </summary>
	public String ToHexString() {
		return ToByteArray().ToHexString();
	}

	/// <summary>
	///     Reset the state so that a new set of data can be input without being affected by previous sets.
	/// </summary>
	/// <remarks>
	///     Typically this is called after retrieving a computed CRC (using ToByteArray() for example) and before calling
	///     Append for a new computation run.
	/// </remarks>
	public void Clear() {
		// Initialise current
		Current = IsOutputReflected ? ReverseBits(Initial, Width) : Initial;
	}


	private static UInt64 ReverseBits(UInt64 value, Int32 valueLength) {
		UInt64 output = 0;

		for (var i = valueLength - 1; i >= 0; i--) {
			output |= (value & 1) << i;
			value >>= 1;
		}

		return output;
	}
}
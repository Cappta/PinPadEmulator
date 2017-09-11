using System;

namespace PinPadEmulator.Extensions
{
	internal static class RandomExtensions
	{
		public static byte[] ByteArray(this Random random, int length)
		{
			var byteArray = new byte[length];
			random.NextBytes(byteArray);
			return byteArray;
		}
	}
}

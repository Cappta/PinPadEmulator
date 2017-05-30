using System;

namespace PinPadEmulator.Extensions
{
	public static class IntExtension
	{
		public static string ToString(this int value, int length)
		{
			var stringValue = value.ToString().PadLeft(length, '0');

			if(stringValue.Length > length) { throw new ArgumentOutOfRangeException($"Value as string exceeded length"); }

			return stringValue;
		}
	}
}

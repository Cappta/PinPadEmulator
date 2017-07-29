using System.Linq;

namespace PinPadEmulator.Extensions
{
	public static class ByteArrayExtension
	{
		public static string ToHexString(this byte[] data)
		{
			return string.Join("", data.Select(d => d.ToString("X2")));
		}
	}
}

using System.Collections.Generic;
using System.Text;

namespace PinPadEmulator
{
	public static class Checksum
	{
		public const int LENGTH = 2;
		private const ushort MASK = 0x1021;

		public static IEnumerable<byte> Encapsulate(string command)
		{
			yield return ByteFlag.PACKET_START;
			foreach (var data in Encoding.ASCII.GetBytes(command)) { yield return data; }
			yield return ByteFlag.PACKET_END;
			foreach (var data in Generate(command)) { yield return data; }
		}

		public static byte[] Generate(string command)
		{
			var data = new List<byte>(Encoding.ASCII.GetBytes(command));
			data.Add(ByteFlag.PACKET_END);

			var wCRC = default(ushort);

			for (var index = 0; index < data.Count; index++)
			{
				var wData = (ushort)(data[index] << 8);
				for (var i = 0; i < 8; i++, wData <<= 1)
				{
					if (((wCRC ^ wData) & 0x8000) <= 0) { wCRC <<= 1; continue; }

					wCRC = (ushort)((wCRC << 1) ^ MASK);
				}
			}

			return new byte[] { (byte)(wCRC >> 8), (byte)wCRC };
		}
	}
}

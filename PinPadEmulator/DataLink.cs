using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadEmulator
{
	public class DataLink
	{
		private const byte PACKET_START = 0x16;
		private const byte PACKET_END = 0x17;
		private const byte ABORT = 0x18;

		private const int CHECKSUM_LENGTH = 2;
		private const ushort CHECKSUM_MASK = 0x1021;

		private StringBuilder stringBuilder;
		private byte[] checksum = new byte[CHECKSUM_LENGTH];
		private int checksumIndex;

		public event Action<string> CommandReceived;
		public event Action<string> CorruptCommandReceived;
		public event Action AbortRequested;

		public Action<byte> Input;

		public DataLink()
		{
			this.Input = this.DataInput;
		}

		public IEnumerable<byte> Encapsulate(string command)
		{
			yield return PACKET_START;
			foreach (var data in Encoding.ASCII.GetBytes(command)) { yield return data; }
			yield return PACKET_END;
			foreach (var data in this.GenerateChecksum(command)) { yield return data; }
		}

		private void DataInput(byte data)
		{
			switch (data)
			{
				case PACKET_START:
					this.stringBuilder = new StringBuilder();
					break;

				case PACKET_END:
					this.checksumIndex = 0;
					this.Input = this.ChecksumInput;
					break;

				case ABORT:
					this.AbortRequested?.Invoke();
					break;

				default:
					this.stringBuilder?.Append(Encoding.ASCII.GetChars(new byte[] { data }));
					break;
			}
		}

		private void ChecksumInput(byte data)
		{
			this.checksum[this.checksumIndex++] = data;
			if (this.checksumIndex != CHECKSUM_LENGTH) { return; }

			this.Input = this.DataInput;

			var requestContent = this.stringBuilder.ToString();
			this.stringBuilder = null;

			var calculatedChecksum = this.GenerateChecksum(requestContent);
			if (calculatedChecksum.SequenceEqual(this.checksum)) { this.CommandReceived(requestContent); }
			else { this.CorruptCommandReceived(requestContent); }
		}

		private byte[] GenerateChecksum(string command)
		{
			var data = new List<byte>(Encoding.ASCII.GetBytes(command));
			data.Add(PACKET_END);

			var wCRC = default(ushort);

			for (var index = 0; index < data.Count; index++)
			{
				var wData = (ushort)(data[index] << 8);
				for (var i = 0; i < 8; i++, wData <<= 1)
				{
					if (((wCRC ^ wData) & 0x8000) <= 0) { wCRC <<= 1; continue; }

					wCRC = (ushort)((wCRC << 1) ^ CHECKSUM_MASK);
				}
			}

			return new byte[] { (byte)(wCRC >> 8), (byte)wCRC };
		}
	}
}

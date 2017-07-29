using System;
using System.Linq;
using System.Text;

namespace PinPadEmulator
{
	public class DataLink
	{
		private StringBuilder stringBuilder;
		private byte[] checksum = new byte[Checksum.LENGTH];
		private int checksumIndex;

		public event Action<string> CommandReceived;
		public event Action<string> CorruptCommandReceived;
		public event Action AbortRequested;

		private Action<byte> input;

		public DataLink()
		{
			this.input = this.DataInput;
		}

		public void Input(params byte[] dataCollection)
		{
			foreach (var data in dataCollection)
			{
				this.input(data);
			}
		}

		private void DataInput(byte data)
		{
			switch (data)
			{
				case ByteFlag.PACKET_START:
					this.stringBuilder = new StringBuilder();
					break;

				case ByteFlag.PACKET_END:
					this.checksumIndex = 0;
					this.input = this.ChecksumInput;
					break;

				case ByteFlag.ABORT:
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
			if (this.checksumIndex != Checksum.LENGTH) { return; }

			this.input = this.DataInput;

			var requestContent = this.stringBuilder.ToString();
			this.stringBuilder = null;

			var calculatedChecksum = Checksum.Generate(requestContent);
			if (calculatedChecksum.SequenceEqual(this.checksum)) { this.CommandReceived(requestContent); }
			else { this.CorruptCommandReceived(requestContent); }
		}
	}
}

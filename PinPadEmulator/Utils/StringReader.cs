using System;

namespace PinPadEmulator.Utils
{
	public class StringReader
	{
		public StringReader(string value)
		{
			if (string.IsNullOrEmpty(value)) { throw new ArgumentNullException(nameof(value)); }

			this.Value = value;
		}

		public string Value { get; private set; }

		public int Offset { get; private set; }

		public int Remaining { get { return this.Value.Length - this.Offset; } }

		public string Read(int length)
		{
			if (length > this.Remaining) { throw new ArgumentOutOfRangeException(nameof(length)); }
			if (length == 0) { return default(string); }

			var value = this.Value.Substring(this.Offset, length);
			this.Offset += length;
			return value;
		}

		public void Seek(int length)
		{
			if (length > this.Remaining || length < -this.Offset) { throw new ArgumentOutOfRangeException(nameof(length)); }

			this.Offset += length;
		}
	}
}

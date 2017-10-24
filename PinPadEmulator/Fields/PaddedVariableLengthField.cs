using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Fields
{
	public class PaddedVariableLengthField<type> : VariableLengthField<type>
	{
		public PaddedVariableLengthField(int headerLength, int contentLength) : base(headerLength)
		{
			this.ContentLength = contentLength;

			if (typeof(type) == typeof(byte[])) { this.PaddingChar = '0'; }
		}

		public int ContentLength { get; }

		public char PaddingChar { get; } = ' ';

		private int TotalLength { get { return this.HeaderLength + this.ContentLength; } }

		public override void Init(StringReader stringReader)
		{
			var contentLength = this.ReadRawHeader(stringReader);

			var content = stringReader.Read(contentLength);
			this.Value = content.ConvertTo<type>();

			stringReader.Seek(this.ContentLength - content.Length);
		}

		public override string ToString()
		{
			var converted = base.ToString();

			if(converted.Length > this.TotalLength) { throw new ArgumentOutOfRangeException($"Exceeded field length"); }

			return converted.PadRight(this.TotalLength, this.PaddingChar);
		}
	}
}

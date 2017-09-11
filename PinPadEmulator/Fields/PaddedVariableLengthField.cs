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
		}

		public int ContentLength { get; }

		private int TotalLength { get { return this.HeaderLength + this.ContentLength; } }

		public override void Init(StringReader stringReader)
		{
			var headerContent = stringReader.Read(this.HeaderLength);
			var contentLength = headerContent.ConvertTo<int>();

			var content = stringReader.Read(contentLength);
			this.Value = content.ConvertTo<type>();

			stringReader.Seek(this.ContentLength - content.Length);
		}

		public override string ToString()
		{
			var converted = base.ToString();

			if(converted.Length > this.TotalLength) { throw new ArgumentOutOfRangeException($"Exceeded field length"); }

			return converted.PadRight(this.TotalLength, ' ');
		}
	}
}

using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Fields
{
	public class HexVariableLengthField : VariableLengthField<byte[]>
	{
		public HexVariableLengthField(int headerLength, Nullable<int> maximumContentLength = default(Nullable<int>))
			: base(headerLength, maximumContentLength)
		{
		}

		protected override int ReadRawHeader(StringReader stringReader)
		{
			return base.ReadRawHeader(stringReader) * 2;
		}

		protected override int CalculateHeaderLength(string converted)
		{
			return base.CalculateHeaderLength(converted) / 2;
		}
	}
}

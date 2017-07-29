using PinPadEmulator.Utils;
using System;
using System.Globalization;

namespace PinPadEmulator.Fields
{
	public class OptionalDateTimeField : Field<DateTime?>
	{
		public readonly string Format;
		public readonly string invalidValue;

		public OptionalDateTimeField(string format)
		{
			this.Format = format;
			this.invalidValue = new string('0', this.Format.Length);
		}

		public override void Init(StringReader stringReader)
		{
			var content = stringReader.Read(this.Format.Length);
			if(content == this.invalidValue) { this.Value = null; return; }
			this.Value = DateTime.ParseExact(content, this.Format, CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			if(this.Value.HasValue == false) { return this.invalidValue; }
			return this.Value.Value.ToString(this.Format);
		}
	}
}

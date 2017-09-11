using PinPadEmulator.Utils;
using System;
using System.Globalization;

namespace PinPadEmulator.Fields
{
	public class DateTimeField : Field<DateTime>
	{
		public DateTimeField(string format)
		{
			this.Format = format;
		}

		public string Format { get; }

		public override void Init(StringReader stringReader)
		{
			var content = stringReader.Read(this.Format.Length);
			this.Value = DateTime.ParseExact(content, this.Format, CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			return this.Value.ToString(this.Format);
		}
	}
}

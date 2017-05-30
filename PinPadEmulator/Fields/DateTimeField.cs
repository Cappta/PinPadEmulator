using PinPadEmulator.Utils;
using System;
using System.Globalization;

namespace PinPadEmulator.Fields
{
	public class DateTimeField : Field<DateTime>
	{
		public readonly string Format;

		public DateTimeField(string format)
		{
			this.Format = format;
		}

		public override void Deserialize(StringReader stringReader)
		{
			var content = stringReader.Read(this.Format.Length);
			this.Value = DateTime.ParseExact(content, this.Format, CultureInfo.InvariantCulture);
		}

		protected override string Serialize(DateTime value)
		{
			return value.ToString(this.Format);
		}
	}
}

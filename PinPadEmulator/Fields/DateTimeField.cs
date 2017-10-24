using PinPadEmulator.Utils;
using System;
using System.Globalization;

namespace PinPadEmulator.Fields
{
	public class DateTimeField : Field<DateTime?>
	{
		private bool optional;

		public DateTimeField(string format)
		{
			this.Format = format;
			this.InvalidValue = new string('0', this.Format.Length);
		}

		public string Format { get; }
		public string InvalidValue { get; }

		public DateTimeField Optional()
		{
			this.optional = true;

			return this;
		}

		public override void Init(StringReader stringReader)
		{
			var content = stringReader.Read(this.Format.Length);
			if(this.optional && content == this.InvalidValue) { this.Value = null; return; }
			this.Value = DateTime.ParseExact(content, this.Format, CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			if(this.optional && this.Value.HasValue == false) { return this.InvalidValue; }
			return this.Value.Value.ToString(this.Format);
		}
	}
}

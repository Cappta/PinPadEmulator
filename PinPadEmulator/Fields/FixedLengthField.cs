using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Fields
{
	public class FixedLengthField<type> : Field<type>
	{
		private bool optional;

		public FixedLengthField(int length)
		{
			this.Length = length;
		}

		public int Length { get; }

		public FixedLengthField<type> Optional()
		{
			this.optional = true;

			return this;
		}

		public override void Init(StringReader stringReader)
		{
			if (this.optional && stringReader.Remaining < this.Length) { return; }

			var content = stringReader.Read(this.Length);
			this.Value = content.ConvertTo<type>();
		}

		public override string ToString()
		{
			if(this.optional && (this.Value == null || this.Value.Equals(default(type)))) { return string.Empty; }

			var value = this.PadString(base.ToString());

			if (value.Length > this.Length) { throw new InvalidOperationException($"Value length exceeds the field limit of {this.Length}"); }

			return value;
		}

		private string PadString(string value)
		{
			if (typeof(type) == typeof(byte[])) { return value.PadRight(this.Length, '0'); }

			if (this.Value?.IsNumericType() == true) { return value.PadLeft(this.Length, '0'); }

			return value.PadRight(this.Length, ' ');
		}
	}
}

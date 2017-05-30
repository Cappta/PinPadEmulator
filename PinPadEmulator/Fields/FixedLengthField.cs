using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public class FixedLengthField<type> : Field<type>
	{
		public readonly int Length;
		private bool optional;

		public FixedLengthField(int length)
		{
			this.Length = length;
		}

		public FixedLengthField<type> Optional()
		{
			this.optional = true;

			return this;
		}

		public override void Deserialize(StringReader stringReader)
		{
			if(this.optional && stringReader.Remaining < this.Length) { return; }

			var content = stringReader.Read(this.Length);
			this.Value = content.ConvertTo<type>();
		}

		protected override string Serialize(type value)
		{
			var converted = base.Serialize(value);

			if (value.IsNumericType()) { return converted.PadLeft(this.Length, '0'); }

			return converted.PadRight(this.Length, ' ');
		}
	}
}

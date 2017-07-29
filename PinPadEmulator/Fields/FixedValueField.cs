using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Fields
{
	public class FixedValueField<type> : IField
	{
		public readonly Field<type> field;

		public FixedValueField(type value, Field<type> field)
		{
			this.field = field;
			this.field.Value = value;
		}

		public type Value
		{
			get
			{
				return this.field.Value;
			}
		}

		public void Init(StringReader stringReader)
		{
			var originalValue = this.field.Value;
			try
			{
				this.field.Init(stringReader);
				if (this.field.Value.Equals(originalValue) == false)
				{
					throw new InvalidOperationException("Cannot change the value of an FixedValueField");
				}
			}
			catch
			{
				this.field.Value = originalValue;
				throw;
			}
		}

		public override string ToString()
		{
			return this.field.ToString();
		}
	}
}

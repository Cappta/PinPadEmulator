using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public abstract class Field<type> : IField
	{
		private type value;
		private string serialized;

		public type Value
		{
			get { return this.value; }
			set
			{
				this.serialized = this.Serialize(value);
				this.value = value;
			}
		}

		public string Serialized
		{
			get
			{
				if (this.serialized == null) { this.serialized = this.Serialize(this.value); }

				return this.serialized;
			}
		}

		public abstract void Deserialize(StringReader stringReader);

		protected virtual string Serialize(type value)
		{
			return value.Serialize();
		}
	}
}

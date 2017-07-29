using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public abstract class Field<type> : IField
	{
		public type Value { get; set; }

		public abstract void Init(StringReader stringReader);

		public override string ToString()
		{
			return this.Value.Serialize();
		}
	}
}

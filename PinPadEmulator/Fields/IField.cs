using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public interface IField
	{
		string Serialized { get; }

		void Deserialize(StringReader stringReader);
	}
}

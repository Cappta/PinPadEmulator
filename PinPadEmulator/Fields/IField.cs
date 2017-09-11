using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public interface IField
	{
		void Init(StringReader stringReader);

		string ToString();
	}
}

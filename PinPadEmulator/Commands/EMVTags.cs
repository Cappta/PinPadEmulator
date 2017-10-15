using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class EmvTags : CommandBlock
	{
		public HexVariableLengthField Tags { get; } = new HexVariableLengthField(3, 256);
	}
}

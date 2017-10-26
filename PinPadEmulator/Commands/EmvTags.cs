using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class EmvTags : CommandBlock
	{
		public VariableLengthField<byte[]> Tags { get; } = new VariableLengthField<byte[]>(3, 256);
	}
}

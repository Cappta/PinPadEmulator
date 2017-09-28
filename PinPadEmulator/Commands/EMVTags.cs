using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class EMVTags : CommandBlock
	{
		public VariableLengthField<byte[]> Tags { get; } = new VariableLengthField<byte[]>(3, 256);
	}
}

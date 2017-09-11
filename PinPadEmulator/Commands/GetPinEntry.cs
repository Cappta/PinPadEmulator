using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class GetPinEntry : FieldGroup
	{
		public FixedLengthField<int> MinimumLength { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> MaximumLength { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> Message { get; } = new FixedLengthField<string>(32);
	}
}

using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class GetPinEntry : FieldGroup
	{
		public readonly FixedLengthField<int> MinimumLength = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> MaximumLength = new FixedLengthField<int>(2);
		public readonly FixedLengthField<string> Message = new FixedLengthField<string>(32);
	}
}

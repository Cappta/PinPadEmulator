using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class AidEntryReference : FieldGroup
	{
		public readonly FixedLengthField<int> AcquirerId = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> RecordIndex = new FixedLengthField<int>(2);
	}
}

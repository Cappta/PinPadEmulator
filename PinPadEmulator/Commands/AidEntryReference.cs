using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class AidEntryReference : FieldGroup
	{
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> RecordIndex { get; } = new FixedLengthField<int>(2);
	}
}

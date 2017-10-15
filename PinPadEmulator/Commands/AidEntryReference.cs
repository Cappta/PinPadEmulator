using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class AidEntryReference : FieldGroup
	{
		public AidEntryReference()
		{
		}

		public AidEntryReference(int acquirerId, int recordIndex)
		{
			this.AcquirerId.Value = acquirerId;
			this.RecordIndex.Value = recordIndex;
		}

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> RecordIndex { get; } = new FixedLengthField<int>(2);
	}
}

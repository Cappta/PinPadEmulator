using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class AidNullEntry : FieldGroup
	{
		public FixedValueField<int> TableId { get; } = new FixedValueField<int>(1, new FixedLengthField<int>(1));
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);

		public FixedLengthField<string> MandatoryNullBlock { get; } = new FixedLengthField<string>(2 + 32 + 2 + 16 + 2 + 4 + 4 + 4 + 3 + 3 + 1 + 15 + 4 + 8 + 6 + 10 + 2 + 10 + 10 + 10 + 8 + 1 + 1 + 1 + 8 + 8 + 8 + 4 + 1 + 40 + 40 + 8);
		public FixedLengthField<string> OptionalNullBlock { get; } = new FixedLengthField<string>(10 + 10 + 10).Optional();
	}
}

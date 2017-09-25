using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableCAPKEntry : FieldGroup
	{
		public FixedLengthField<int> Length { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TableId { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> RID { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> CAPKId { get; } = new FixedLengthField<string>(2);
		public FixedValueField<string> Reserved { get; } = new FixedValueField<string>("00", new FixedLengthField<string>(2));
		public PaddedVariableLengthField<string> PublicKeyExponent { get; } = new PaddedVariableLengthField<string>(1, 6);
		public PaddedVariableLengthField<string> PublicKeyModulus { get; } = new PaddedVariableLengthField<string>(3, 496);
		public FixedLengthField<bool> HasChecksum { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<string> PublicKeChecksum { get; } = new FixedLengthField<string>(40);
		public FixedValueField<string> Reserved2 { get; } = new FixedValueField<string>(new string('0', 42), new FixedLengthField<string>(42));

	}
}

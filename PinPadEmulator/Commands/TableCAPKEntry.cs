using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableCAPKEntry : FieldGroup
	{
		public FixedLengthField<int> Length { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TableId { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> RID { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<byte[]> CAPKId { get; } = new FixedLengthField<byte[]>(2);
		public FixedValueField<string> Reserved { get; } = new FixedValueField<string>("00", new FixedLengthField<string>(2));
		public PaddedVariableLengthField<byte[]> PublicKeyExponent { get; } = new PaddedVariableLengthField<byte[]>(1, 6);
		public PaddedVariableLengthField<byte[]> PublicKeyModulus { get; } = new PaddedVariableLengthField<byte[]>(3, 496);
		public FixedLengthField<bool> HasChecksum { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<byte[]> PublicKeChecksum { get; } = new FixedLengthField<byte[]>(40);
		public FixedLengthField<string> Reserved2 { get; } = new FixedLengthField<string>(42);

	}
}

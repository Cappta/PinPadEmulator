using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableRevokedCertificateEntry : FieldGroup
	{
		public FixedLengthField<int> Length { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TableId { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> RID { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> PublicKeyId { get; } = new FixedLengthField<string>(2);
		public FixedLengthField<string> CertificateSerialNumber { get; } = new FixedLengthField<string>(6);
	}
}

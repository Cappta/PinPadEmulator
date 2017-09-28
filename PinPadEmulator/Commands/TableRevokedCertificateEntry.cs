using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableRevokedCertificateEntry : FieldGroup
	{
		public FixedLengthField<int> Length { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TableId { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> RID { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<byte[]> PublicKeyId { get; } = new FixedLengthField<byte[]>(2);
		public FixedLengthField<byte[]> CertificateSerialNumber { get; } = new FixedLengthField<byte[]>(6);
	}
}

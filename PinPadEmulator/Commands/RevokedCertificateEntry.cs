using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class RevokedCertificateEntry : FieldGroup
	{
		public FixedValueField<int> TableId { get; } = new FixedValueField<int>(3, new FixedLengthField<int>(1));
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> RID { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> PublicKeyId { get; } = new FixedLengthField<string>(2);
		public FixedLengthField<string> CertificateSerialNumber { get; } = new FixedLengthField<string>(6);
	}
}

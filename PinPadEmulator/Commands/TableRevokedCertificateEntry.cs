using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableRevokedCertificateEntry : TableEntry
	{
		public FixedLengthField<string> RID { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> PublicKeyId { get; } = new FixedLengthField<string>(2);
		public FixedLengthField<string> CertificateSerialNumber { get; } = new FixedLengthField<string>(6);
	}
}

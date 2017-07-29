using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetPinRequest : BaseRequest
	{
		public override string Identifier => "GPN";

		public readonly FixedLengthField<CryptoMethod> Method = new FixedLengthField<CryptoMethod>(1);
		public readonly FixedLengthField<int> MasterKeyIndex = new FixedLengthField<int>(2);
		public readonly FixedLengthField<byte[]> EncryptedWorkingKey = new FixedLengthField<byte[]>(32);
		public readonly VariableLengthField<string> Pan = new VariableLengthField<string>(2, 19);
		public readonly FieldList<GetPinEntry> Entries = new FieldList<GetPinEntry>(1);
	}
}

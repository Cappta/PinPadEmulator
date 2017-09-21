using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetPinRequest : BaseRequest
	{
		public override string Identifier => "GPN";

		public FixedLengthField<CryptoMethod> Method { get; } = new FixedLengthField<CryptoMethod>(1);
		public FixedLengthField<int> MasterKeyIndex { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> EncryptedWorkingKey { get; } = new FixedLengthField<byte[]>(32);
		public PaddedVariableLengthField<string> Pan { get; } = new PaddedVariableLengthField<string>(2, 19);
		public FieldList<GetPinEntry> Entries { get; } = new FieldList<GetPinEntry>(1);
	}
}

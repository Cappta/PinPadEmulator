using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class EncryptBufferRequest : BaseRequest
	{
		public override string Identifier => "ENB";

		public FixedLengthField<CryptoMethod> Method { get; } = new FixedLengthField<CryptoMethod>(1);
		public FixedLengthField<int> MasterKeyIndex { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> WorkingKey { get; } = new FixedLengthField<byte[]>(32);
		public FixedLengthField<byte[]> DecryptedData { get; } = new FixedLengthField<byte[]>(16);
	}
}

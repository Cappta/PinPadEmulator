using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class EncryptBufferResponse : BaseResponse
	{
		public override string Identifier => "ENB";

		public FixedLengthField<byte[]> EncryptedData { get; } = new FixedLengthField<byte[]>(16);
	}
}

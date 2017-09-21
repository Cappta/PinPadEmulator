using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetPinResponse : BaseResponse
	{
		public override string Identifier => "GPN";

		public FixedLengthField<byte[]> EncryptedPin { get; } = new FixedLengthField<byte[]>(16);
		public FixedLengthField<byte[]> KeySerialNumber { get; } = new FixedLengthField<byte[]>(20);
	}
}

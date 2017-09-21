using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetDUKPTResponse : BaseResponse
	{
		public override string Identifier => "GDU";

		public FixedLengthField<byte[]> KeySerialNumber { get; } = new FixedLengthField<byte[]>(20);
	}
}

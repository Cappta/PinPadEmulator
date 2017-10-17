using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class NotificationMessageResponse : BaseResponse
	{
		public override string Identifier => "NTM";

		public FixedLengthField<string> Message { get; } = new FixedLengthField<string>(29);
	}
}

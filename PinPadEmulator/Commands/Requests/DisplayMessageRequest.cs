using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class DisplayMessageRequest : BaseRequest
	{
		public override string Identifier => "DSP";

		public FixedLengthField<string> Message { get; } = new FixedLengthField<string>(32);
	}
}

using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class CloseRequest : BaseRequest
	{
		public override string Identifier => "CLO";

		public FixedLengthField<string> Message { get; } = new FixedLengthField<string>(32);
	}
}

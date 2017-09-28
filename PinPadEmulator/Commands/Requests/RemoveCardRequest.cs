using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class RemoveCardRequest : BaseRequest
	{
		public override string Identifier => "RMC";

		public FixedLengthField<string> Message { get; } = new FixedLengthField<string>(32);
	}
}

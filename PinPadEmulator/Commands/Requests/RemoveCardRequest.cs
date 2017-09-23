using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class RemoveCardRequest : BaseRequest
	{
		public override string Identifier => "RMC";

		public VariableLengthField<string> Message { get; } = new VariableLengthField<string>(3, 32);
	}
}

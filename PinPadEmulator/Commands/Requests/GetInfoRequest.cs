using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetInfoRequest : BaseRequest
	{
		public override string Identifier => "GIN";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
	}
}

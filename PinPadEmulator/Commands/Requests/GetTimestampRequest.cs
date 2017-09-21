using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetTimestampRequest : BaseRequest
	{
		public override string Identifier => "GTS";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
	}
}

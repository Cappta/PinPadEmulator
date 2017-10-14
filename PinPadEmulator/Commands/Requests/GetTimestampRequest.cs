using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetTimestampRequest : BaseRequest
	{
		public GetTimestampRequest()
		{
		}

		public GetTimestampRequest(int acquirerId)
		{
			this.AcquirerId.Value = acquirerId;
		}

		public override string Identifier => "GTS";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
	}
}

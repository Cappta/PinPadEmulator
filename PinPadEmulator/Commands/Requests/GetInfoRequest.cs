using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetInfoRequest : BaseRequest
	{
		public GetInfoRequest()
		{
		}

		public GetInfoRequest(int acquirerId)
		{
			this.AcquirerId.Value = acquirerId;
		}

		public override string Identifier => "GIN";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
	}
}

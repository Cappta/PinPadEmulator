using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetTimestampResponse : BaseResponse
	{
		public override string Identifier => "GTS";

		public DateTimeField Timestamp { get; } = new DateTimeField("ddMMyyyy");
		public FixedLengthField<int> SequentialNumber { get; } = new FixedLengthField<int>(2);
	}
}

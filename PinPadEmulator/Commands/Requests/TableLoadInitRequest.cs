using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class TableLoadInitRequest : BaseRequest
	{
		public override string Identifier => "TLI";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public DateTimeField Timestamp { get; } = new DateTimeField("ddMMyyyy");
		public FixedLengthField<int> SequentialNumber { get; } = new FixedLengthField<int>(2);
	}
}

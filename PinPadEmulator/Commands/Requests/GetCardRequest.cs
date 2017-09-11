using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetCardRequest : BaseRequest
	{
		public override string Identifier => "GCR";

		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> TargetAid { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> TransactionAmount { get; } = new FixedLengthField<int>(12);
		public DateTimeField TransactionDateTime { get; } = new DateTimeField("yyMMddHHmmss");
		public FixedLengthField<string> TableVersion { get; } = new FixedLengthField<string>(10);
		public FieldList<AidEntryReference> AidEntryReferences { get; } = new FieldList<AidEntryReference>(2);
		public FixedLengthField<bool?> ContactlessOn { get; } = new FixedLengthField<bool?>(1).Optional();
	}
}

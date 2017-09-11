using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetCardRequest : BaseRequest
	{
		public override string Identifier => "GCR";

		public readonly FixedLengthField<int> AcquirerId = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> TargetAid = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> TransactionAmount = new FixedLengthField<int>(12);
		public readonly DateTimeField TransactionDateTime = new DateTimeField("yyMMddHHmmss");
		public readonly FixedLengthField<string> TableVersion = new FixedLengthField<string>(10);
		public readonly FieldList<AidEntryReference> AidEntryReferences = new FieldList<AidEntryReference>(2);
		public readonly FixedLengthField<bool?> ContactlessOn = new FixedLengthField<bool?>(1).Optional();
	}
}

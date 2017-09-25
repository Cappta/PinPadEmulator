using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class FinishChipResponse : BaseResponse
	{
		public override string Identifier => "FNC";

		public FixedLengthField<TransactionResult> TransactionResult { get; } = new FixedLengthField<TransactionResult>(1);
		public HexVariableLengthField EMVData { get; } = new HexVariableLengthField(3, 512);
		public HexVariableLengthField IssuerScriptResults { get; } = new HexVariableLengthField(2);
		public VariableLengthField<string> AcquirerData { get; } = new VariableLengthField<string>(3);
	}
}

using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class FinishChipResponse : BaseResponse
	{
		public override string Identifier => "FNC";

		public FixedLengthField<TransactionResult> TransactionResult { get; } = new FixedLengthField<TransactionResult>(1);
		public VariableLengthField<byte[]> EMVData { get; } = new VariableLengthField<byte[]>(3, 512);
		public VariableLengthField<byte[]> IssuerScriptResults { get; } = new VariableLengthField<byte[]>(2);
		public VariableLengthField<string> AcquirerData { get; } = new VariableLengthField<string>(3);
	}
}

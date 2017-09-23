using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class FinishChipRequest : BaseRequest
	{
		public override string Identifier => "FNC";

		public FixedLengthField<bool> Success { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<IssuerType> IssuerType { get; } = new FixedLengthField<IssuerType>(1);
		public FixedLengthField<string> AuthorizationResponseCode { get; } = new FixedLengthField<string>(2);
		public VariableLengthField<byte[]> EMVData { get; } = new VariableLengthField<byte[]>(3, 512);
		public VariableLengthField<string> AcquirerData { get; } = new VariableLengthField<string>(3);
		public VariableLengthField<string> EMVTags { get; } = new VariableLengthField<string>(3, 259);
	}
}

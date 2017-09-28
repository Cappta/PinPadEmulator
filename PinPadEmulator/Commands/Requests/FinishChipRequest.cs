using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class FinishChipRequest : BaseRequest
	{
		public override string Identifier => "FNC";

		public FixedLengthField<int> Success { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<IssuerType> IssuerType { get; } = new FixedLengthField<IssuerType>(1);
		public FixedLengthField<string> AuthorizationResponseCode { get; } = new FixedLengthField<string>(2);
		public VariableLengthField<byte[]> EMVData { get; } = new VariableLengthField<byte[]>(3, 512);
		public FixedValueField<int> AcquirerData { get; } = new FixedValueField<int>(0, new FixedLengthField<int>(3));
		public EMVTags Tags { get; } = new EMVTags();
	}
}

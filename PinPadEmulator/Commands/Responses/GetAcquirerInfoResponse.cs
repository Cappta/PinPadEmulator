using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetAcquirerInfoResponse : BaseResponse
	{
		public override string Identifier => "GIN";

		public FixedLengthField<string> AcquirerName { get; } = new FixedLengthField<string>(20);
		public FixedLengthField<string> AcquirerApplicationVersion { get; } = new FixedLengthField<string>(13);
		public FixedLengthField<string> AcquirerInfo { get; } = new FixedLengthField<string>(7);
		public VariableLengthField<byte[]> AcquirerSAMId { get; } = new VariableLengthField<byte[]>(2);
	}
}

using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class DefineRsaWorkingKeyRequest : BaseRequest
	{
		public override string Identifier => "DWK";

		public FixedValueField<int> Mode { get; } = new FixedValueField<int>(2, new FixedLengthField<int>(1));
		public FixedLengthField<byte[]> Modulus { get; } = new FixedLengthField<byte[]>(256);
		public FixedLengthField<byte[]> Exponent { get; } = new FixedLengthField<byte[]>(6);
	}
}

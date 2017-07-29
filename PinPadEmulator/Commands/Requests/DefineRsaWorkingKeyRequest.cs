using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class DefineRsaWorkingKeyRequest : BaseRequest
	{
		public override string Identifier => "DWK";

		public readonly FixedValueField<int> Mode = new FixedValueField<int>(2, new FixedLengthField<int>(1));
		public readonly FixedLengthField<byte[]> Modulus = new FixedLengthField<byte[]>(256);
		public readonly FixedLengthField<byte[]> Exponent = new FixedLengthField<byte[]>(6);
	}
}

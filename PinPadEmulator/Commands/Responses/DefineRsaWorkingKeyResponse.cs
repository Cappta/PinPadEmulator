using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class DefineRsaWorkingKeyResponse : BaseResponse
	{
		public override string Identifier => "DWK";

		public readonly FixedLengthField<byte[]> Cryptogram = new FixedLengthField<byte[]>(256);
	}
}

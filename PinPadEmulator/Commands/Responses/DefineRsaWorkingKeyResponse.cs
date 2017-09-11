using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class DefineRsaWorkingKeyResponse : BaseResponse
	{
		public override string Identifier => "DWK";

		public FixedLengthField<byte[]> Cryptogram { get; } = new FixedLengthField<byte[]>(256);
	}
}

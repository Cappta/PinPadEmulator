using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class DefineRsaWorkingKeyResponse : BaseResponse
	{
		public override string Identifier => "DWK";

		public DefineRsaWorkingKeyResponse() { }

		public DefineRsaWorkingKeyResponse(byte[] cryptogram)
		{
			this.Cryptogram.Value = cryptogram;
		}

		public FixedLengthField<byte[]> Cryptogram { get; } = new FixedLengthField<byte[]>(256);
	}
}

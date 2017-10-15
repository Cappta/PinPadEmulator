using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetDUKPTRequest : BaseRequest
	{
		public GetDUKPTRequest()
		{
		}

		public GetDUKPTRequest(CryptoMethod method, int registerId)
		{
			this.CryptoMode.Value = method;
			this.RegisterId.Value = registerId;
		}

		public override string Identifier => "GDU";

		public FixedLengthField<CryptoMethod> CryptoMode { get; } = new FixedLengthField<CryptoMethod>(1);
		public FixedLengthField<int> RegisterId { get; } = new FixedLengthField<int>(2);
	}
}

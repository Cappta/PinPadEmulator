using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GetDUKPTRequest : BaseRequest
	{
		public override string Identifier => "GDU";

		public FixedLengthField<int> CryptoMode { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<int> RegisterId { get; } = new FixedLengthField<int>(2);
	}
}

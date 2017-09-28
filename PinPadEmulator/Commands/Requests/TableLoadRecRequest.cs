using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class TableLoadRecRequest : BaseRequest
	{
		public override string Identifier => "TLR";

		public FieldTableLoadRegitryList Entries { get; } = new FieldTableLoadRegitryList(2);
	}
}

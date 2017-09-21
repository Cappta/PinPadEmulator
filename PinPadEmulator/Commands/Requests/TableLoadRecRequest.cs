using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class TableLoadRecRequest : BaseRequest
	{
		public override string Identifier => "TLR";

		public FieldList<TableEntry> Entries { get; } = new FieldList<TableEntry>(2);
	}
}

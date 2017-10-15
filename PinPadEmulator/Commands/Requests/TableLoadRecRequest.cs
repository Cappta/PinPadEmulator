using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class TableLoadRecRequest : BaseRequest
	{
		public override string Identifier => "TLR";

		public FieldList<TableField> Entries { get; } = new FieldList<TableField>(2);
	}
}

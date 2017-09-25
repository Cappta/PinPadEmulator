using PinPadEmulator.Commands;
using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;

namespace PinPadEmulator.Fields
{
	public class FieldTableLoadRegitryList : FieldList<FieldGroup>
	{
		public FieldTableLoadRegitryList(int headerLength) 
			: base(headerLength)
		{
		}

		public override void Init(StringReader stringReader)
		{
			var headerContent = stringReader.Read(this.HeaderLength);
			var fieldCount = headerContent.ConvertTo<int>();

			var entries = new FieldGroup[fieldCount];
			for (var i = 0; i < fieldCount; i++)
			{
				var tableId = stringReader.Read(4);

				if (tableId.EndsWith("1")) { entries[i] = new TableAIDEntry(); }
				else if (tableId.EndsWith("2")) { entries[i] = new TableCAPKEntry(); }
				else if (tableId.EndsWith("3")) { entries[i] = new TableRevokedCertificateEntry(); }

				stringReader.Seek(-4);

				entries[i].Init(stringReader);
			}

			this.Clear();
			base.AddRange(entries);
		}
	}
}

using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System.Collections.Generic;
using System.Text;

namespace PinPadEmulator.Fields
{
	public class CommandBlock : IField
	{
		private const int HEADER_LENGTH = 3;
		private readonly List<IField> fieldCollection = new List<IField>();

		public void Append(IField field)
		{
			this.fieldCollection.Add(field);
		}

		public void Init(StringReader stringReader)
		{
			var headerContent = stringReader.Read(HEADER_LENGTH);
			var contentLength = headerContent.ConvertTo<int>();

			var content = stringReader.Read(contentLength);
			var contentReader = new StringReader(content);
			
			var tagFields = new List<IField>();

			foreach (var field in this.fieldCollection)
			{
				if (contentReader.Remaining == 0) { tagFields.Add(field); }
				else { field.Init(contentReader); }
			}

			if (stringReader.Remaining == 0) { return; }

			var tags = stringReader.Read(stringReader.Remaining);
			var tagsReader = new StringReader(content);

			foreach (var field in tagFields) { field.Init(tagsReader); }
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			foreach (var field in this.fieldCollection) { stringBuilder.Append(field.ToString()); }
			stringBuilder.Insert(0, stringBuilder.Length.ToString(HEADER_LENGTH));

			return stringBuilder.ToString();
		}
	}
}

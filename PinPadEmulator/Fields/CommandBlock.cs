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

		public string Serialized
		{
			get
			{
				var stringBuilder = new StringBuilder();

				foreach (var field in this.fieldCollection) { stringBuilder.Append(field.Serialized); }
				stringBuilder.Insert(0, stringBuilder.Length.ToString(HEADER_LENGTH));

				return stringBuilder.ToString();
			}
		}

		public void Append(IField field)
		{
			this.fieldCollection.Add(field);
		}

		public void Deserialize(StringReader stringReader)
		{
			var headerContent = stringReader.Read(HEADER_LENGTH);
			var contentLength = headerContent.ConvertTo<int>();

			var content = stringReader.Read(contentLength);
			var contentReader = new StringReader(content);

			foreach (var field in this.fieldCollection) { field.Deserialize(contentReader); }
		}
	}
}

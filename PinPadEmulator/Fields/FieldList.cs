using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System.Collections.Generic;
using System.Text;

namespace PinPadEmulator.Fields
{
	public class FieldList<type> : List<type>, IField where type : IField, new()
	{
		public FieldList(int headerLength)
		{
			this.HeaderLength = headerLength;
		}

		public int HeaderLength { get; }

		public virtual void Init(StringReader stringReader)
		{
			var headerContent = stringReader.Read(this.HeaderLength);
			var fieldCount = headerContent.ConvertTo<int>();

			var newItems = new type[fieldCount];
			for (var i = 0; i < fieldCount; i++)
			{
				newItems[i] = new type();
				newItems[i].Init(stringReader);
			}

			this.Clear();
			base.AddRange(newItems);
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append(this.Count.ToString(this.HeaderLength));
			foreach (var item in this)
			{
				stringBuilder.Append(item.Serialize());
			}

			return stringBuilder.ToString();
		}

		public new void Add(type item)
		{
			item.Serialize();
			base.Add(item);
		}

		public new void AddRange(IEnumerable<type> collection)
		{
			foreach (var item in collection) { item.Serialize(); }
			base.AddRange(collection);
		}

		public new void Insert(int index, type item)
		{
			item.Serialize();
			base.Insert(index, item);
		}

		public new void InsertRange(int index, IEnumerable<type> collection)
		{
			foreach (var item in collection) { item.Serialize(); }
			base.InsertRange(index, collection);
		}
	}
}

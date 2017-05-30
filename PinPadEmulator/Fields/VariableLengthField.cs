using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Fields
{
	public class VariableLengthField<type> : Field<type>
	{
		public readonly int HeaderLength;
		public readonly Nullable<int> MaximumContentLength;

		public VariableLengthField(int headerLength, Nullable<int> maximumContentLength = default(Nullable<int>))
		{
			this.HeaderLength = headerLength;
			this.MaximumContentLength = maximumContentLength;
		}

		public override void Deserialize(StringReader stringReader)
		{
			var headerContent = stringReader.Read(this.HeaderLength);
			var contentLength = headerContent.ConvertTo<int>();

			var content = stringReader.Read(contentLength);

			if (this.MaximumContentLength.HasValue && content.Length > this.MaximumContentLength)
			{
				throw new ArgumentOutOfRangeException($"This field has a limit of {this.MaximumContentLength}");
			}

			this.Value = content.ConvertTo<type>();
		}

		protected override string Serialize(type value)
		{
			var converted = base.Serialize(value);

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(converted.Length.ToString(this.HeaderLength));
			stringBuilder.Append(converted);

			return stringBuilder.ToString();
		}
	}
}

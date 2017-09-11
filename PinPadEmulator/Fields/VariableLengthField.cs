using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Fields
{
	public class VariableLengthField<type> : Field<type>
	{
		public VariableLengthField(int headerLength, Nullable<int> maximumContentLength = default(Nullable<int>))
		{
			this.HeaderLength = headerLength;
			this.MaximumContentLength = maximumContentLength;
		}

		public int HeaderLength { get; }
		public Nullable<int> MaximumContentLength { get; }

		public override void Init(StringReader stringReader)
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

		public override string ToString()
		{
			var converted = base.ToString();

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(converted.Length.ToString(this.HeaderLength));
			stringBuilder.Append(converted);

			return stringBuilder.ToString();
		}
	}
}

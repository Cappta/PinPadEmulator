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

		public virtual int HeaderLength { get; }
		public Nullable<int> MaximumContentLength { get; }

		public override void Init(StringReader stringReader)
		{
			int contentLength = ReadRawHeader(stringReader);

			var content = stringReader.Read(contentLength);

			if (this.MaximumContentLength.HasValue && content.Length > this.MaximumContentLength)
			{
				throw new ArgumentOutOfRangeException($"This field has a limit of {this.MaximumContentLength}");
			}

			this.Value = content.ConvertTo<type>();
		}

		protected int ReadRawHeader(StringReader stringReader)
		{
			var headerContent = stringReader.Read(this.HeaderLength);
			var contentLength = headerContent.ConvertTo<int>();

			if (typeof(type) == typeof(byte[])) { contentLength *= 2; }

			return contentLength;
		}

		public override string ToString()
		{
			var converted = base.ToString();
			var header = this.CalculateHeaderLength(converted);

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(header.ToString(this.HeaderLength));
			stringBuilder.Append(converted);

			return stringBuilder.ToString();
		}

		protected int CalculateHeaderLength(string converted)
		{
			if (typeof(type) == typeof(byte[])) { return converted.Length / 2; }

			return converted.Length;
		}
	}
}

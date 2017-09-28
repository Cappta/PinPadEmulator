using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Fields
{
    public class VariableLengthField<type> : Field<type>
    {
        public VariableLengthField(int headerLength)
        {
            this.HeaderLength = headerLength;
        }

        public VariableLengthField(int headerLength, int maximumContentLength)
            : this(headerLength)
        {
            this.MaximumContentLength = maximumContentLength;
        }

        public virtual int HeaderLength { get; }

        public Nullable<int> MaximumContentLength { get; }

        public override void Init(StringReader stringReader)
        {
            int contentLength = ReadContentLength(stringReader);

            var content = stringReader.Read(contentLength);

            if (this.MaximumContentLength.HasValue && content.Length > this.MaximumContentLength)
            {
                throw new ArgumentOutOfRangeException($"This field has a limit of {this.MaximumContentLength}");
            }

            this.Value = content.ConvertTo<type>();
        }

        protected int ReadContentLength(StringReader stringReader)
        {
            var headerContent = stringReader.Read(this.HeaderLength);
            var header = headerContent.ConvertTo<int>();

            return typeof(type) == typeof(byte[]) ? header * 2 : header;
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
             => typeof(type) == typeof(byte[])? converted.Length / 2 : converted.Length;
    }
}

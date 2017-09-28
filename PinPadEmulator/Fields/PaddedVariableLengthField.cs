using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Fields
{
    public class PaddedVariableLengthField<type> : VariableLengthField<type>
    {
        public PaddedVariableLengthField(int headerLength, int contentLength) : base(headerLength)
        {
            this.ContentLength = contentLength;
        }

        public int ContentLength { get; }

        private int TotalLength { get { return this.HeaderLength + this.ContentLength; } }

        public override void Init(StringReader stringReader)
        {
            int contentLength = ReadContentLength(stringReader);

            var content = stringReader.Read(contentLength);
            this.Value = content.ConvertTo<type>();

            stringReader.Seek(this.ContentLength - content.Length);
        }

        public override string ToString()
        {
            var converted = base.ToString();

            if (converted.Length > this.TotalLength) { throw new ArgumentOutOfRangeException($"Exceeded field length"); }

            return typeof(type) == typeof(byte[])
                ? converted.PadRight(this.TotalLength, '0')
                : converted.PadRight(this.TotalLength, ' ');
        }
    }
}

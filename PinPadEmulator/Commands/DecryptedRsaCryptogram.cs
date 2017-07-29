using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class DecryptedRsaCryptogram : FieldGroup
	{
		public readonly FixedValueField<string> Header = new FixedValueField<string>("T", new FixedLengthField<string>(1));
		public readonly FixedValueField<int> Version = new FixedValueField<int>(1, new FixedLengthField<int>(1));
		public readonly FixedLengthField<int> SequentialNumber = new FixedLengthField<int>(9);
		public readonly FixedLengthField<byte[]> WorkingKey = new FixedLengthField<byte[]>(32);
		public readonly FixedValueField<int> Padding = new FixedValueField<int>(0, new FixedLengthField<int>(84));
		public readonly FixedValueField<string> Footer = new FixedValueField<string>("X", new FixedLengthField<string>(1));
	}
}

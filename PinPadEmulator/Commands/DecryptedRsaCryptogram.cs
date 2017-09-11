using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class DecryptedRsaCryptogram : FieldGroup
	{
		public FixedValueField<string> Header { get; } = new FixedValueField<string>("T", new FixedLengthField<string>(1));
		public FixedValueField<int> Version { get; } = new FixedValueField<int>(1, new FixedLengthField<int>(1));
		public FixedLengthField<int> SequentialNumber { get; } = new FixedLengthField<int>(9);
		public FixedLengthField<byte[]> WorkingKey { get; } = new FixedLengthField<byte[]>(32);
		public FixedValueField<int> Padding { get; } = new FixedValueField<int>(0, new FixedLengthField<int>(84));
		public FixedValueField<string> Footer { get; } = new FixedValueField<string>("X", new FixedLengthField<string>(1));
	}
}

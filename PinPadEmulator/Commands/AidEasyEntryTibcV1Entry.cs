using PinPadEmulator.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinPadEmulator.Commands
{
	public class AidEasyEntryTibcV1Entry : FieldGroup
	{
		public FixedValueField<int> TableId { get; } = new FixedValueField<int>(1, new FixedLengthField<int>(1));
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public PaddedVariableLengthField<byte[]> AID { get; } = new PaddedVariableLengthField<byte[]>(2, 32);
		public FixedLengthField<int> ApplicationType { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> ApplicationLabel { get; } = new FixedLengthField<string>(16);
		public FixedValueField<ApplicationPattern> TableApplicationPattern { get; } = new FixedValueField<ApplicationPattern>(ApplicationPattern.EasyEntryTibcV1, new FixedLengthField<ApplicationPattern>(2));
	}
}

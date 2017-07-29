using PinPadEmulator.Extensions;
using PinPadEmulator.Fields;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Commands.Responses
{
	public abstract class BaseResponse : BaseCommand
	{
		public readonly FixedLengthField<int> Status = new FixedLengthField<int>(3);

		public override void Init(StringReader stringReader)
		{
			var identifier = stringReader.Read(IDENTIFIER_LENGTH);
			if (this.Identifier.EqualsIgnoreCase(identifier) == false)
			{
				throw new ArgumentException($"Expected identifier {this.Identifier} but received {identifier} instead", nameof(stringReader));
			}

			this.Status.Init(stringReader);
			foreach (var commandBlock in this.CommandBlocks)
			{
				commandBlock.Init(stringReader);
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append(this.Identifier);
			stringBuilder.Append(this.Status.ToString());
			foreach (var commandBlock in this.CommandBlocks)
			{
				stringBuilder.Append(commandBlock.ToString());
			}

			return stringBuilder.ToString();
		}
	}
}

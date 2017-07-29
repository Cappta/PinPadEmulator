using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Commands.Requests
{
	public abstract class BaseRequest : BaseCommand
	{
		public override void Init(StringReader stringReader)
		{
			var identifier = stringReader.Read(IDENTIFIER_LENGTH);
			if (this.Identifier.EqualsIgnoreCase(identifier) == false)
			{
				throw new ArgumentException($"Expected identifier {this.Identifier} but received {identifier} instead", nameof(stringReader));
			}

			foreach (var commandBlock in this.CommandBlocks)
			{
				commandBlock.Init(stringReader);
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append(this.Identifier);

			foreach (var commandBlock in this.CommandBlocks)
			{
				stringBuilder.Append(commandBlock.ToString());
			}

			return stringBuilder.ToString();
		}
	}
}

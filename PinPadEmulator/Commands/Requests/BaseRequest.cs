using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;

namespace PinPadEmulator.Commands.Requests
{
	public abstract class BaseRequest : BaseCommand
	{
		public void Init(StringReader stringReader)
		{
			this.Command = stringReader.Value;

			var identifier = stringReader.Read(IDENTIFIER_LENGTH);
			if (this.Identifier.EqualsIgnoreCase(identifier) == false)
			{
				throw new ArgumentException($"Expected identifier {this.Identifier} but received {identifier} instead", nameof(stringReader));
			}

			foreach (var commandBlock in this.CommandBlocks)
			{
				commandBlock.Deserialize(stringReader);
			}
		}

		public string Command { get; private set; }
	}
}

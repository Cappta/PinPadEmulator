using PinPadEmulator.Fields;
using System.Text;

namespace PinPadEmulator.Commands.Responses
{
	public abstract class BaseResponse : BaseCommand
	{
		public readonly FixedLengthField<int> Status = new FixedLengthField<int>(3);

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append(this.Identifier);
			stringBuilder.Append(this.Status.Serialized);

			foreach (var commandBlock in this.CommandBlocks)
			{
				stringBuilder.Append(commandBlock.Serialized);
			}

			return stringBuilder.ToString();
		}
	}
}

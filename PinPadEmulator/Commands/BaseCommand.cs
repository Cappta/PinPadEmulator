using PinPadEmulator.Fields;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PinPadEmulator.Commands
{
	public abstract class BaseCommand
	{
		protected const int IDENTIFIER_LENGTH = 3;

		private readonly FieldInfo[] commandFieldInfos;

		public BaseCommand()
		{
			var typeofIField = typeof(IField);

			var members = this.GetType().GetMembers();
			var fields = members.OfType<FieldInfo>();
			var implementedFields = fields.Where(field => field.DeclaringType?.BaseType != typeof(BaseCommand));
			this.commandFieldInfos = implementedFields.Where(field => typeofIField.IsAssignableFrom(field.FieldType)).ToArray();
		}

		public abstract string Identifier { get; }
		
		protected IEnumerable<CommandBlock> CommandBlocks
		{
			get
			{
				if (this.commandFieldInfos.Count() == 0) { yield break; }

				var commandBlock = new CommandBlock();
				foreach (var fieldInfo in this.commandFieldInfos)
				{
					var field = fieldInfo.GetValue(this) as IField;
					commandBlock.Append(field);
				}
				yield return commandBlock;
			}
		}
	}
}

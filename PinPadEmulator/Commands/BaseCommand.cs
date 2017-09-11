using PinPadEmulator.Fields;
using PinPadEmulator.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PinPadEmulator.Commands
{
	public abstract class BaseCommand
	{
		protected const int IDENTIFIER_LENGTH = 3;

		public abstract string Identifier { get; }

		private readonly PropertyInfo[] commandPropInfos;

		public BaseCommand()
		{
			var typeofIField = typeof(IField);

			var members = this.GetType().GetMembers();
			var properties = members.OfType<PropertyInfo>();
			var implementedProps = properties.Where(field => field.DeclaringType?.BaseType != typeof(BaseCommand));
			this.commandPropInfos = implementedProps.Where(field => typeofIField.IsAssignableFrom(field.PropertyType)).ToArray();
		}

		public abstract void Init(StringReader stringReader);

		protected IEnumerable<CommandBlock> CommandBlocks
		{
			get
			{
				if (this.commandPropInfos.Count() == 0) { yield break; }

				var commandBlock = new CommandBlock();
				foreach (var fieldInfo in this.commandPropInfos)
				{
					var field = fieldInfo.GetGetMethod().Invoke(this, null) as IField;
					commandBlock.Append(field);
				}
				yield return commandBlock;
			}
		}
	}
}

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

		private readonly IEnumerable<PropertyInfo> fields;

		public BaseCommand()
		{
			this.fields = this.ExtractFields(this);
		}

		public abstract string Identifier { get; }

		protected IEnumerable<CommandBlock> CommandBlocks
		{
			get
			{
				if (this.fields.Count() == 0) { yield break; }

				var fields = this.ExtractSimpleFields();
				if (fields.Count() > 0) { yield return this.CreateMainCommandBlock(fields); }

				var commandBlockProps = this.ExtractCommandBlocks();
				if (commandBlockProps.Count() == 0) { yield break; }

				foreach (var commandBlockProp in commandBlockProps)
				{
					yield return CreateSubCommandBlock(commandBlockProp);
				}
			}
		}

		private IEnumerable<PropertyInfo> ExtractSimpleFields()
			=> this.fields.Where(field => typeof(CommandBlock).IsAssignableFrom(field.PropertyType) == false);

		private IEnumerable<PropertyInfo> ExtractCommandBlocks()
			=> this.fields.Where(field => typeof(CommandBlock).IsAssignableFrom(field.PropertyType));

		private CommandBlock CreateMainCommandBlock(IEnumerable<PropertyInfo> props)
		{
			var commandBlock = new CommandBlock();

			foreach (var prop in props)
			{
				var field = prop.GetValue(this, null) as IField;
				commandBlock.Append(field);
			}

			return commandBlock;
		}

		private CommandBlock CreateSubCommandBlock(PropertyInfo commandBlockProp)
		{
			var commandBlock = commandBlockProp.GetValue(this, null) as CommandBlock;

			var props = this.ExtractFields(commandBlock);

			foreach (var prop in props)
			{
				var field = prop.GetValue(commandBlock, null) as IField;
				commandBlock.Append(field);
			}

			return commandBlock;
		}

		private IEnumerable<PropertyInfo> ExtractFields(object instance)
			=> instance.GetType().GetProperties().Where(field
				=> field.DeclaringType?.BaseType != typeof(BaseCommand)
				&& typeof(IField).IsAssignableFrom(field.PropertyType));

		public abstract void Init(StringReader stringReader);
	}
}

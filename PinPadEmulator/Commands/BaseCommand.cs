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
			this.CommandBlocks = new List<CommandBlock>();

			this.fields = this.ExtractFields(this);

			var simpleFields = this.ExtractSimpleFields();
			if (simpleFields.Count() > 0) { this.CommandBlocks.Add(this.CreateMainCommandBlock(simpleFields)); }

			var commandBlocks = this.ExtractCommandBlocks();
			foreach (var commandBlock in commandBlocks)
			{
				this.CommandBlocks.Add(CreateSubCommandBlock(commandBlock));
			}
		}

		public abstract string Identifier { get; }

		protected ICollection<CommandBlock> CommandBlocks { get; }

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

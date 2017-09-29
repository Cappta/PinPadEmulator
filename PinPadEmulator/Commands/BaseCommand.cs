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

        public BaseCommand()
        {
            this.CommandBlocks = new List<CommandBlock>();

            var fields = this.ExtractFields(this);
            if (fields.Count() == 0) { return; }

            var simpleFields = this.ExtractSimpleFields(fields);
            if (fields.Count() > 0) { this.CommandBlocks.Add(this.CreateMainCommandBlock(simpleFields)); }

            var commandBlockProps = this.ExtractCommandBlocks(fields);
            if (commandBlockProps.Count() == 0) { return; }

            foreach (var commandBlockProp in commandBlockProps)
            {
                this.CommandBlocks.Add(CreateSubCommandBlock(commandBlockProp));
            }
        }

        public abstract string Identifier { get; }

        protected ICollection<CommandBlock> CommandBlocks { get; }

        private IEnumerable<PropertyInfo> ExtractSimpleFields(IEnumerable<PropertyInfo> fields)
            => fields.Where(field => typeof(CommandBlock).IsAssignableFrom(field.PropertyType) == false);

        private IEnumerable<PropertyInfo> ExtractCommandBlocks(IEnumerable<PropertyInfo> fields)
            => fields.Where(field => typeof(CommandBlock).IsAssignableFrom(field.PropertyType));

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

using PinPadEmulator.Utils;
using System.Collections.Generic;
using System.Linq;

namespace PinPadEmulator.Fields
{
	public class FieldGroup : IField
	{
		private readonly IEnumerable<IField> commandProps;

		public FieldGroup()
		{
			var props = this.GetType().GetProperties()
				.Where(prop => typeof(IField).IsAssignableFrom(prop.PropertyType));

			this.commandProps = props.Select(prop => prop.GetValue(this, null) as IField);
		}

		public void Init(StringReader stringReader)
		{
			foreach (var field in this.commandProps)
			{
				field.Init(stringReader);
			}
		}

		public override string ToString()
		{
			return string.Join("", this.commandProps.Select(field => field.ToString()));
		}
	}
}

using PinPadEmulator.Utils;
using System.Linq;
using System.Reflection;

namespace PinPadEmulator.Fields
{
	public class FieldGroup : IField
	{
		private readonly IField[] commandProps;

		public FieldGroup()
		{
			var members = this.GetType().GetMembers();
			var properties = members.OfType<PropertyInfo>();
			var commandPropInfos = properties.Where(field => field.PropertyType.GetInterfaces().Contains(typeof(IField))).ToArray();
			this.commandProps = commandPropInfos.Select(fieldInfo => fieldInfo.GetGetMethod().Invoke(this, null) as IField).ToArray();
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

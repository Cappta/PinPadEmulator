using PinPadEmulator.Utils;
using System.Linq;
using System.Reflection;

namespace PinPadEmulator.Fields
{
	public class FieldGroup : IField
	{
		private readonly IField[] commandFields;

		public FieldGroup()
		{
			var members = this.GetType().GetMembers();
			var fields = members.OfType<FieldInfo>();
			var commandFieldInfos = fields.Where(field => field.FieldType.GetInterfaces().Contains(typeof(IField))).ToArray();
			this.commandFields = commandFieldInfos.Select(fieldInfo => fieldInfo.GetValue(this) as IField).ToArray();
		}

		public void Init(StringReader stringReader)
		{
			foreach (var field in this.commandFields)
			{
				field.Init(stringReader);
			}
		}

		public override string ToString()
		{
			return string.Join("", this.commandFields.Select(field => field.ToString()));
		}
	}
}

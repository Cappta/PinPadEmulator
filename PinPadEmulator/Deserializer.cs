using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;
using System;
using System.Linq;

namespace PinPadEmulator
{
	public class Deserializer
	{
		private readonly Type[] requestTypes;

		public Deserializer()
		{
			var typeOfBaseRequest = typeof(BaseRequest);

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var types = assemblies.SelectMany(s => s.GetTypes());
			this.requestTypes = types.Where(type => typeOfBaseRequest.IsAssignableFrom(type) && type.IsAbstract == false).ToArray();
		}

		public BaseRequest Deserialize(string command)
		{
			var commandReader = new StringReader(command);
			foreach (var requestType in this.requestTypes)
			{
				commandReader.Seek(-commandReader.Offset);

				var instance = Activator.CreateInstance(requestType) as BaseRequest;
				try
				{
					instance.Init(commandReader);
					return instance;
				}
				catch { /* IGNORE */ }
			}
			return null;
		}
	}
}

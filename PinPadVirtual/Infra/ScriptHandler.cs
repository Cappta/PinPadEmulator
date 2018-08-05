using System.Collections.Generic;
using System.IO;

namespace PinPadVirtual.Infra
{
	public class ScriptHandler
	{
		public static IEnumerable<string> GetScripts()
		{
			var path = @"Scripts/";

			var files = Directory.GetFiles(path);

			return files;
		}
	}
}

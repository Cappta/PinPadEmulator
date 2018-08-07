
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PinPadVirtual.Infra
{
	public class ScriptFile
	{
		private string name;
		private string path;

		public string Name { get => this.name; private set => this.name = value; }
		public string Path { get => this.path; private set => this.path = value; }

		public static List<ScriptFile> GetScripts()
		{
			var pathOfScripts = @"Scripts/";

			var scriptsFiles = Directory.GetFiles(pathOfScripts);

			var scripts = new List<ScriptFile>();

			foreach (var item in scriptsFiles)
			{
				var splited = item.Split('/');

				var name = splited.Last().Substring(0, splited.Last().Length - 4);

				var script = new ScriptFile
				{
					Path = item,

					Name = name
				};

				scripts.Add(script);
			}

			return scripts;
		}
	}
}

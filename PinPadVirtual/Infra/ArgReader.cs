using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PinPadVirtual.Infra
{
	public class ArgReader
	{
		public ArgReader(string[] args)
		{
			if (args == null) { throw new ArgumentNullException(nameof(args)); }

			this.RegexPatterns = this.LoadRegexPatterns(args);
			this.Flags = this.LoadWithIdentifier(args, "+");
			this.SerialPorts = this.LoadWithIdentifier(args, "@");
		}

		public Dictionary<Regex, string> RegexPatterns { get; }
		public string[] Flags { get; }
		public string[] SerialPorts { get; }

		private Dictionary<Regex, string> LoadRegexPatterns(string[] args)
		{
			var regexPatterns = new Dictionary<Regex, string>();
			for (var index = 0; index < args.Length;)
			{
				var currentArg = args[index++];
				if (currentArg.StartsWith("-") == false) { continue; }

				var regex = new Regex(currentArg.Substring(1));
				var pattern = args[index++];

				regexPatterns[regex] = pattern;
			}
			return regexPatterns;
		}

		private string[] LoadWithIdentifier(string[] args, string identifier)
		{
			var values = new List<string>();
			for (var index = 0; index < args.Length;)
			{
				var currentArg = args[index++];
				if (currentArg.StartsWith(identifier) == false) { continue; }

				var value = currentArg.Substring(identifier.Length);

				values.Add(value.ToUpper());
			}
			return values.ToArray();
		}
	}

}

using PinPadEmulator.Crypto;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PinPadEmulator.Devices
{
	public class RegexSimulatedDevice : VirtualDevice
	{
		public event Action<Regex, string, string> RegexApplied;

		private Dictionary<Regex, string> regexResponsePatternDictionary;

		public RegexSimulatedDevice(ICryptoHandler cryptographyHandler, Dictionary<Regex, string> regexPatternDictionary) : base(cryptographyHandler)
		{
			if (regexPatternDictionary == null) { throw new ArgumentNullException(nameof(regexPatternDictionary)); }

			this.regexResponsePatternDictionary = regexPatternDictionary;
			this.CommandReceived += this.OnCommandReceived;
		}

		private void OnCommandReceived(string command)
		{
			var response = command;
			foreach (var regexPatternPair in this.regexResponsePatternDictionary)
			{
				var regex = regexPatternPair.Key;

				if (regex.IsMatch(command))
				{
					var pattern = regexPatternPair.Value;
					response = regex.Replace(response, pattern);

					this.RegexApplied?.Invoke(regex, pattern, response);
				}
			}
			this.Reply(response);
		}
	}
}

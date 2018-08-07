using PinPadEmulator;
using PinPadEmulator.Crypto;
using PinPadEmulator.Devices;
using PinPadSDK.Extensions;
using PinPadSDK.Windows;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace PinPadVirtual.Infra
{
	public class EmulatorHandler
	{
		public static event Action<string> AppendLog;

		public static void ProcessSimulator(ArgReader argReader, string primarySerialPort)
		{
			var regexPatterns = argReader.RegexPatterns;

			var cryptoHandler = new ActiveCryptoHandler();
			cryptoHandler.WorkingKeyDefined += OnWorkingKeyDefined;

			var virtualDevice = new SerialDevice(primarySerialPort);

			var simulatedDevice = new RegexSimulatedDevice(cryptoHandler, regexPatterns);

			simulatedDevice.RegexApplied += OnRegexApplied;
			simulatedDevice.CorruptCommand += OnCorruptCommand;
			simulatedDevice.AbortRequested += OnAbort;

			var interceptor = new Interceptor(virtualDevice, simulatedDevice);
			interceptor.Request += OnRequest;
			interceptor.Response += OnResponse;

			Thread.Sleep(Timeout.Infinite);
		}

		public static void OnWorkingKeyDefined(byte[] workingKey)
		{
			AppendLog.Invoke($"WorkingKey definida para \"{workingKey.ToHexString()}\"");
		}

		public static void OnRequest(string request)
		{
			AppendLog.Invoke($"Recebido \"{request}\"");
		}

		public static void OnRegexApplied(Regex regex, string pattern, string result)
		{
			AppendLog.Invoke($"Regex \"{regex}\" com padrão \"{pattern}\" gerou \"{result}\"");
		}

		public static void OnResponse(string response)
		{
			AppendLog.Invoke($"Respondido \"{response}\"");
		}

		public static void OnCorruptCommand(string invalidRequest)
		{
			AppendLog.Invoke($"Checksum invalido para: \"{invalidRequest}\"");
		}

		public static void OnAbort()
		{
			AppendLog.Invoke($"Tentativa de abortar comando anterior");
		}
	}
}

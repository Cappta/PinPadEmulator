using PinPadEmulator;
using PinPadEmulator.Crypto;
using PinPadEmulator.Devices;
using PinPadSDK.Extensions;
using PinPadSDK.Windows;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace PinPadMockerCLI
{
	class Program
	{
		private static readonly Regex SERIALPORT_NAME_REGEX = new Regex("COM/d+");

		static void Main(string[] args)
		{
			var argReader = new ArgReader(args);
			RecoverSerialPorts(argReader, out var primarySerialPort, out var secondarySerialPort);
			if (string.IsNullOrWhiteSpace(secondarySerialPort))
			{
                ProcessSimulator(argReader, primarySerialPort);
			}
			else
			{
                ProcessInterceptor(primarySerialPort, secondarySerialPort);
            }
		}

		static void RecoverSerialPorts(ArgReader argReader, out string primarySerialPort, out string secondarySerialPort)
		{
			switch (argReader.SerialPorts.Length)
			{
				case 0:
					Console.WriteLine("Which serial port to attach to?");
					primarySerialPort = Console.ReadLine();

					Console.WriteLine("Which serial port to connect to?");
					secondarySerialPort = Console.ReadLine();
					return;
				case 1:
					primarySerialPort = argReader.SerialPorts[0];
					secondarySerialPort = string.Empty;
					return;
				default:
					primarySerialPort = argReader.SerialPorts[0];
					secondarySerialPort = argReader.SerialPorts[1];
					return;
			}
		}

		static void ProcessInterceptor(string primarySerialPort, string secondarySerialPort)
		{
			var cryptoHandler = new PassiveCryptoHandler();
			cryptoHandler.WorkingKeyDefined += OnWorkingKeyDefined;
			var virtualDevice = new DecryptedDevice(cryptoHandler, new SerialDevice(primarySerialPort));
			var realDevice = new DecryptedDevice(cryptoHandler, new SerialDevice(secondarySerialPort));

			var interceptor = new Interceptor(virtualDevice, realDevice);
			interceptor.Request += OnRequest;
			interceptor.Response += OnResponse;

			AppendLog("Started");
			Thread.Sleep(Timeout.Infinite);
		}

		static void ProcessSimulator(ArgReader argReader, string primarySerialPort)
		{
			var regexPatterns = RecoverRegexPatterns(argReader);
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

			AppendLog("Started");
			Thread.Sleep(Timeout.Infinite);
		}

		static Dictionary<Regex, string> RecoverRegexPatterns(ArgReader argReader)
		{
			if (argReader.RegexPatterns.Count > 0) { return argReader.RegexPatterns; }

			var regexPatterns = new Dictionary<Regex, string>();
			while (true)
			{
				Console.WriteLine("Input the regex or blank to stop");
				var regexString = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(regexString)) { return regexPatterns; }

				var regex = new Regex(regexString);
				var pattern = Console.ReadLine();
				regexPatterns[regex] = pattern;
			}
		}

		static void OnWorkingKeyDefined(byte[] workingKey)
		{
			AppendLog($"WorkingKey definida para \"{workingKey.ToHexString()}\"");
		}

		static void OnRequest(string request)
		{
			AppendLog($"Recebido \"{request}\"");
		}

		static void OnRegexApplied(Regex regex, string pattern, string result)
		{
			AppendLog($"Regex \"{regex}\" com padrão \"{pattern}\" gerou \"{result}\"");
		}

		static void OnResponse(string response)
		{
			AppendLog($"Respondido \"{response}\"");
		}

		static void OnCorruptCommand(string invalidRequest)
		{
			AppendLog($"Checksum invalido para: \"{invalidRequest}\"");
		}

		static void OnAbort()
		{
			AppendLog($"Tentativa de abortar comando anterior");
		}

		static void AppendLog(string message)
		{
			var logTime = DateTime.Now.ToLongTimeString();
			var logMessage = $"{logTime} : {message}{Environment.NewLine}";

			Console.WriteLine(logMessage);
		}
	}
}

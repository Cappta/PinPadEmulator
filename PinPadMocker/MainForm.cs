using PinPadEmulator;
using PinPadEmulator.Crypto;
using PinPadEmulator.Devices;
using PinPadEmulator.Extensions;
using PinPadMocker.Properties;
using PinPadSDK.Extensions;
using PinPadSDK.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PinPadMocker
{
	public partial class MainForm : Form
	{
		private bool intercepting = false;
		private Interceptor interceptor;

		private Dictionary<string, string> requestResponseDictionary = new Dictionary<string, string>();
		private string currentRequest;

		public MainForm()
		{
			InitializeComponent();
			this.LoadSettings();
			this.FormClosed += this.SaveSettings;
		}

		private void LoadSettings()
		{
			this.UxComboVirtualCom.Text = Settings.Default.VirtualSerialPort;
			this.UxComboRealCom.Text = Settings.Default.RealSerialPort;

			if (Settings.Default.Mode == 0)
			{
				this.UxRadioModeIntercept.Checked = true;
			}
			else
			{
				this.UxRadioModeSimulate.Checked = true;
			}
		}

		private void SaveSettings(object sender, FormClosedEventArgs e)
		{
			Settings.Default.VirtualSerialPort = this.UxComboVirtualCom.Text;
			Settings.Default.RealSerialPort = this.UxComboRealCom.Text;
			Settings.Default.Mode = this.UxRadioModeIntercept.Checked ? 0 : 1;
			Settings.Default.Save();
		}

		private void UxRadioModeListen_CheckedChanged(object sender, EventArgs e)
		{
			this.UxComboRealCom.Enabled = true;
		}

		private void UxRadioModeSimulate_CheckedChanged(object sender, EventArgs e)
		{
			this.UxComboRealCom.Enabled = false;
		}

		private void UxComUpdateTimer_Tick(object sender, EventArgs e)
		{
			var portNames = SerialPort.GetPortNames();

			this.SyncComboBoxItems(this.UxComboVirtualCom, portNames);
			this.SyncComboBoxItems(this.UxComboRealCom, portNames);
		}

		private void SyncComboBoxItems(ComboBox comboBox, string[] items)
		{
			foreach (var item in items)
			{
				if (comboBox.Items.Contains(item) == false)
				{
					comboBox.Items.Add(item);
				}
			}
			for (var index = comboBox.Items.Count - 1; index >= 0; index--)
			{
				var item = comboBox.Items[index];
				if (items.Contains(item) == false)
				{
					comboBox.Items.Remove(item);
				}
			}
		}

		private void UxButtonStart_Click(object sender, EventArgs e)
		{
			if (this.UxButtonStart.Text == "START")
			{
				if (this.UxRadioModeIntercept.Checked) { this.StartIntercepting(); } else { this.StartSimulating(); }
				this.DisableUxItems();
				this.UxButtonStart.Text = "STOP";
				this.AppendLog("Started");
			}
			else
			{
				this.StopInterceptor();
				this.EnableUxItems();
				this.UxButtonStart.Text = "START";
				this.AppendLog("Stopped");
			}
		}

		private void DisableUxItems()
		{
			this.UxComboVirtualCom.Enabled = false;
			this.UxComboRealCom.Enabled = false;
			this.UxRadioModeIntercept.Enabled = false;
			this.UxRadioModeSimulate.Enabled = false;
		}

		private void EnableUxItems()
		{
			this.UxComboVirtualCom.Enabled = true;
			this.UxComboRealCom.Enabled = this.UxRadioModeIntercept.Checked;
			this.UxRadioModeIntercept.Enabled = true;
			this.UxRadioModeSimulate.Enabled = true;
		}

		private void StartSimulating()
		{
			this.intercepting = false;
			var commands = new Dictionary<Regex, string>();

			foreach (var requestResponse in this.requestResponseDictionary)
			{
				var request = requestResponse.Key;
				var response = requestResponse.Value;
				commands[new Regex(request)] = response;
			}

			var cryptoHandler = new ActiveCryptoHandler();
			cryptoHandler.WorkingKeyDefined += this.OnWorkingKeyDefined;
			var virtualDevice = new SerialDevice(this.UxComboVirtualCom.Text);
			var simulatedDevice = new RegexSimulatedDevice(cryptoHandler, commands);
			simulatedDevice.RegexApplied += this.OnRegexApplied;
			simulatedDevice.CorruptCommand += this.OnCorruptCommand;
			simulatedDevice.AbortRequested += this.OnAbort;

			this.interceptor = new Interceptor(virtualDevice, simulatedDevice);
			this.interceptor.Request += this.OnRequest;
			this.interceptor.Response += this.OnResponse;
		}

		private void StartIntercepting()
		{
			this.intercepting = true;
			var cryptoHandler = new PassiveCryptoHandler();
			cryptoHandler.WorkingKeyDefined += this.OnWorkingKeyDefined;
			var virtualDevice = new DecryptedDevice(cryptoHandler, new SerialDevice(this.UxComboVirtualCom.Text));
			var realDevice = new DecryptedDevice(cryptoHandler, new SerialDevice(this.UxComboRealCom.Text));

			this.interceptor = new Interceptor(virtualDevice, realDevice);
			this.interceptor.Request += this.OnRequest;
			this.interceptor.Response += this.OnResponse;
		}

		private void OnWorkingKeyDefined(byte[] workingKey)
		{
			this.AppendLog($"WorkingKey definida para \"{workingKey.ToHexString()}\"");
		}

		private void OnRequest(string request)
		{
			this.AppendLog($"Recebido \"{request}\"");
			this.currentRequest = request;
		}

		private void OnRegexApplied(Regex regex, string pattern, string result)
		{
			this.AppendLog($"Regex \"{regex}\" com padrão \"{pattern}\" gerou \"{result}\"");
		}

		private void OnResponse(string response)
		{
			this.AppendLog($"Respondido \"{response}\"");
			if (this.intercepting) { this.requestResponseDictionary[this.currentRequest] = response; }
		}

		private void OnCorruptCommand(string invalidRequest)
		{
			this.AppendLog($"Checksum invalido para: \"{invalidRequest}\"");
		}

		private void OnAbort()
		{
			this.AppendLog($"Tentativa de abortar comando anterior");
		}

		private void AppendLog(string message)
		{
			var logTime = DateTime.Now.ToLongTimeString();
			var logMessage = $"{logTime} : {message}{Environment.NewLine}";

			this.UxTextLog.Invoke(new Action(() =>
			{
				lock (this.UxTextLog) { this.UxTextLog.Text += logMessage; }
			}));
		}

		private void StopInterceptor()
		{
			this.interceptor.Dispose();
		}

		private void UxButtonSave_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog()
			{
				CheckPathExists = true,
				AddExtension = true,
				DefaultExt = "txt",
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
			};
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) { return; }

			using (var file = File.CreateText(dialog.FileName))
			{
				foreach (var requestResponse in this.requestResponseDictionary)
				{
					var request = requestResponse.Key;
					var response = requestResponse.Value;

					file.WriteLine(request);
					file.WriteLine(response);
				}
				this.AppendLog($"Saved {dialog.FileName}");
			}
		}

		private void UxButtonLoad_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog()
			{
				CheckPathExists = true,
				AddExtension = true,
				DefaultExt = "txt",
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
			};
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) { return; }

			this.requestResponseDictionary.Clear();
			using (var file = File.OpenText(dialog.FileName))
			{
				while (file.EndOfStream == false)
				{
					var request = file.ReadLine();
					var response = file.ReadLine();

					this.requestResponseDictionary[request] = response;
				}
				this.AppendLog($"Loaded {dialog.FileName}");
			}
		}

		private void UxButtonReset_Click(object sender, EventArgs e)
		{
			this.requestResponseDictionary.Clear();
			this.UxTextLog.Text = "";
		}
	}
}

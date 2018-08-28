using PinPadVirtual.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PinPadVirtual
{
	public partial class PinPad : Window
	{
		private Thread emulatorThread;
		private List<ScriptFile> scriptFiles;

		private ScriptFile ActualScript { get; set; }
		private int MenuIndex { get; set; }
		private string ActualSerialPort { get; set; }

		public PinPad()
		{
			InitializeComponent();

			this.MenuIndex = 0;

			this.FillSerialPortDisplay();

			EmulatorHandler.AppendLog += this.OnListenMessage;
		}

		private void FillScriptDisplay()
		{
			if (this.ListsDisplay.Items.Count > 0) { this.ListsDisplay.Items.Clear(); }

			this.scriptFiles = ScriptFile.GetScripts();

			foreach (var script in this.scriptFiles)
			{
				this.ListsDisplay.Items.Add(script.Name);
			}
		}

		private void FillSerialPortDisplay()
		{
			var serialPorts = SerialPort.GetPortNames();

			if (serialPorts.Length == 0 )
			{
				MessageBox.Show("Nenhuma porta COM encontrada!");
				Application.Current.Shutdown();
			}

			if (this.ListsDisplay.Items.Count > 0) { this.ListsDisplay.Items.Clear(); }

			foreach (var serialPort in serialPorts)
			{
				this.ListsDisplay.Items.Add(serialPort);
			}
		}

		private string ReadActualScriptFile()
		{
			var scriptPath = this.ActualScript.Path;
			return File.ReadAllText(scriptPath, Encoding.UTF8);
		}

		public void InitializeEmulator()
		{
			var args = SplitArguments(this.ReadActualScriptFile());
			var argReader = new ArgReader(args);

			EmulatorHandler.ProcessSimulator(argReader, this.ActualSerialPort);
		}

		private void InitializeEmulatorThread()
		{
			this.emulatorThread = new Thread(new ThreadStart(this.InitializeEmulator));
			this.emulatorThread.Start();
			this.emulatorThread.Priority = ThreadPriority.Highest;
		}

		private void StopEmulatorThread()
		{
			this.emulatorThread.Abort();
		}

 		public void OnListenMessage(string message)
		{
			this.Dispatcher.Invoke((() => { this.TranslactMessage(message); }));
		}
		
		public static string[] SplitArguments(string commandLine)
		{
			var parmChars = commandLine.ToCharArray();
			var inSingleQuote = false;
			var inDoubleQuote = false;
			for (var index = 0; index < parmChars.Length; index++)
			{
				if (parmChars[index] == '"' && !inSingleQuote)
				{
					inDoubleQuote = !inDoubleQuote;
					parmChars[index] = '\n';
				}
				if (parmChars[index] == '\'' && !inDoubleQuote)
				{
					inSingleQuote = !inSingleQuote;
					parmChars[index] = '\n';
				}
				if (!inSingleQuote && !inDoubleQuote && parmChars[index] == ' ')
					parmChars[index] = '\n';
			}
			return (new string(parmChars)).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		}

		private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			try
			{
				this.DragMove();
			}
			catch (Exception)
			{

			}
		}

		private void LeftArrow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.BackProcess();
		}

		private void RightArrow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.ListsDisplay.SelectedItems.Count <= 0)
			{
				MessageBox.Show("Por favor, selecione um item da lista antes de continuar!");
				return;
			}

			this.ContinueProcess();
		}

		private void ContinueProcess()
		{
			this.MenuIndex++;
			switch (this.MenuIndex)
			{
				case 1:
					this.ActualSerialPort = this.ListsDisplay.SelectedItem.ToString();
					this.FillScriptDisplay();
					this.LeftDisplayButton.Visibility = Visibility.Visible;
					break;
				case 2:
					this.RightDisplayButton.Visibility = Visibility.Hidden;
					this.ListsDisplay.Visibility = Visibility.Hidden;
					this.PinpadTextDisplay.Visibility = Visibility.Visible;
					this.ActualScript = this.scriptFiles[this.ListsDisplay.SelectedIndex];
					this.InitializeEmulatorThread();
					break;
			}
		}

		private void BackProcess()
		{
			switch (this.MenuIndex)
			{
				case 1:
					this.ActualSerialPort = null;
					this.FillSerialPortDisplay();
					this.LeftDisplayButton.Visibility = Visibility.Hidden;
					break;
				case 2:
					this.ActualScript = null;
					this.FillScriptDisplay();
					this.PinpadTextDisplay.Visibility = Visibility.Hidden;
					this.RightDisplayButton.Visibility = Visibility.Visible;
					this.ListsDisplay.Visibility = Visibility.Visible;
					break;
				default:
					break;
			}
			this.MenuIndex--;
		}

		private void TranslactMessage(string message)
		{
			this.PinpadTextDisplay.Text = message;
		}
	}
}

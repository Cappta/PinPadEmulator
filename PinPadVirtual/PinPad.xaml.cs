using PinPadVirtual.Infra;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PinPadVirtual
{
	/// <summary>
	/// Lógica interna para PinPad.xaml
	/// </summary>
	public partial class PinPad : Window
	{
		private string Script;
		private Thread emulatorThread;
		

		public PinPad()
		{
			InitializeComponent();
			this.Script = InitilizeScript();
			EmulatorHandler.AppendLog += this.OnListenMessage;
		}

		private string InitilizeScript()
		{
			return File.ReadAllText(@"Scripts\Master-Padrao.txt", Encoding.UTF8);
		}

		public void InitializeEmulator()
		{
			var args = SplitArguments(this.Script);
			var argReader = new ArgReader(args);
			EmulatorHandler.ProcessSimulator(argReader, "COM11");
		}

		public void OnListenMessage(string message)
		{
			this.Dispatcher.Invoke((Action)(() => { this.PinpadDisplay.Text = message; }));
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
			this.DragMove();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.PinpadDisplay.Text = "Started";
			this.emulatorThread = new Thread(new ThreadStart(InitializeEmulator));
			this.emulatorThread.Start();
			this.emulatorThread.Priority = ThreadPriority.Highest;
		}
	}
}

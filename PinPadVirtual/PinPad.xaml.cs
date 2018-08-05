using PinPadVirtual.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PinPadVirtual
{
	/// <summary>
	/// Lógica interna para PinPad.xaml
	/// </summary>
	public partial class PinPad : Window
	{
		private string Script;

		public PinPad()
		{
			InitializeComponent();
			this.Script = InitilizeScript();
		}

		private string InitilizeScript()
		{
			return File.ReadAllText(@"Scripts\Digitado.txt", Encoding.UTF8);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var args = SplitArguments(this.Script);
			var argReader = new ArgReader(args);
			EmulatorHandler.ProcessSimulator(argReader, "COM198");
		}

		public static void ListenMessage(string message)
		{
			MessageBox.Show(message);
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
	}
}

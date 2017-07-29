using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace PinPadEmulator.Devices
{
	public class SerialDevice : IDevice
	{
		private const int BAUD_RATE = 19200;
		private const Parity PARITY = Parity.None;
		private const int DATA_BITS = 8;
		private const StopBits STOP_BITS = StopBits.One;

		private static readonly TimeSpan READ_TIMEOUT = TimeSpan.FromMilliseconds(100);

		public event Action<byte[]> Output;

		private SerialPort serialPort;

		public SerialDevice(string portName)
		{
			if (string.IsNullOrEmpty(portName)) { throw new ArgumentNullException(nameof(portName)); }

			this.serialPort = new SerialPort(portName, BAUD_RATE, PARITY, DATA_BITS, STOP_BITS);
			this.serialPort.ReadTimeout = (int)READ_TIMEOUT.TotalMilliseconds;
			this.serialPort.DataReceived += this.OnSerialPortDataReceived;
			this.serialPort.Open();
		}

		public void Input(byte[] data)
		{
			this.serialPort.Write(data, 0, data.Length);
		}

		private void OnSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			var data = this.Read().ToArray();
			this.Output?.Invoke(data);
		}

		private IEnumerable<byte> Read()
		{
			while (this.serialPort.BytesToRead > 0)
			{
				yield return (byte)this.serialPort.ReadByte();
			}
		}

		public void Dispose()
		{
			if(this.serialPort != null)
			{
				this.serialPort.Dispose();
				this.serialPort = null;
			}
		}
	}
}

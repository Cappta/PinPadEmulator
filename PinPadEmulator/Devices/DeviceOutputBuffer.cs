using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PinPadEmulator.Devices
{
	public class DeviceOutputBuffer
	{
		private readonly IDevice device;

		private readonly Queue<byte> buffer = new Queue<byte>();
		public TimeSpan ReadTimeout = TimeSpan.FromMilliseconds(100);

		public DeviceOutputBuffer(IDevice device)
		{
			if (device == null) { throw new ArgumentNullException(nameof(device)); }

			this.device = device;
			this.device.Output += this.OnDeviceOutput;
		}

		public IEnumerable<byte> Read(int length)
		{
			var stopwatch = Stopwatch.StartNew();
			var counter = 0;
			while (counter < length && stopwatch.Elapsed < this.ReadTimeout)
			{
				lock (this.buffer)
				{
					if (this.buffer.Count > 0)
					{
						yield return this.buffer.Dequeue();
						counter++;
					}

					Thread.Sleep(1);
				}
			}
		}

		private void OnDeviceOutput(byte[] output)
		{
			lock (this.buffer)
			{
				foreach (var data in output)
				{
					this.buffer.Enqueue(data);
				}
			}
		}
	}
}

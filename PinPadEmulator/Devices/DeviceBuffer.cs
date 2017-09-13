using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PinPadEmulator.Devices
{
	public abstract class DeviceBuffer
	{
		public TimeSpan ReadTimeout = TimeSpan.FromMilliseconds(100);

		public DeviceBuffer(IDevice device)
		{
			this.Device = device ?? throw new ArgumentNullException(nameof(device));
		}

		protected IDevice Device { get; }

		protected Queue<byte> Buffer { get; } = new Queue<byte>();

		public IEnumerable<byte> Read(int length)
		{
			var stopwatch = Stopwatch.StartNew();
			var counter = 0;
			while (counter < length && stopwatch.Elapsed < this.ReadTimeout)
			{
				lock (this.Buffer)
				{
					if (this.Buffer.Count > 0)
					{
						yield return this.Buffer.Dequeue();
						counter++;
					}

					Thread.Sleep(1);
				}
			}
		}
	}
}

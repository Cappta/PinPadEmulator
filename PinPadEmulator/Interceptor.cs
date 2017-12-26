using PinPadSDK;
using PinPadSDK.Devices;
using System;
using System.Diagnostics;
using System.Text;

namespace PinPadEmulator
{
	public class Interceptor : IDisposable
	{
		public event Action<string> Request;
		public event Action<string> Response;

		private DataLink virtualLink;
		private DataLink realLink;

		private IDevice virtualDevice;
		private IDevice realDevice;

		public Interceptor(IDevice virtualDevice, IDevice realDevice)
		{
			if (virtualDevice == null) { throw new ArgumentNullException(nameof(virtualDevice)); }
			if (realDevice == null) { throw new ArgumentNullException(nameof(realDevice)); }

			this.virtualDevice = virtualDevice;
			this.virtualDevice.Output += this.OnVirtualDeviceOutput;

			this.virtualLink = new DataLink();
			this.virtualLink.CommandReceived += this.OnVirtualLinkCommandReceived;

			this.realDevice = realDevice;
			this.realDevice.Output += this.OnRealDeviceOutput;

			this.realLink = new DataLink();
			this.realLink.CommandReceived += this.OnRealLinkCommandReceived;
		}

		private void OnVirtualLinkCommandReceived(string command)
		{
			this.Request?.Invoke(command);
		}

		private void OnRealLinkCommandReceived(string command)
		{
			this.Response?.Invoke(command);
		}

		private void OnVirtualDeviceOutput(byte[] dataCollection)
		{
			Debug.WriteLine($"OnVirtualDeviceOutput: {Encoding.ASCII.GetString(dataCollection)} | {BitConverter.ToString(dataCollection)}");
			
			this.virtualLink.Input(dataCollection);
			this.realDevice.Input(dataCollection);
		}

		private void OnRealDeviceOutput(byte[] dataCollection)
		{
			Debug.WriteLine($"OnRealDeviceOutput: {Encoding.ASCII.GetString(dataCollection)} | {BitConverter.ToString(dataCollection)}");
			
			this.realLink.Input(dataCollection);
			this.virtualDevice.Input(dataCollection);
		}

		public void Dispose()
		{
			if (this.virtualDevice != null)
			{
				this.virtualDevice.Dispose();
				this.virtualDevice = null;
			}
			if (this.realDevice != null)
			{
				this.realDevice.Dispose();
				this.realDevice = null;
			}
		}
	}
}

using PinPadEmulator.Crypto;
using PinPadSDK;
using PinPadSDK.Devices;
using System;
using System.Linq;

namespace PinPadEmulator.Devices
{
	public class DecryptedDevice : IDevice
	{
		public event Action<byte[]> Output;

		public ICryptoHandler CryptographyHandler;

		private readonly DataLink inputLink;
		private readonly DataLink outputLink;
		private readonly IDevice device;

		public DecryptedDevice(ICryptoHandler cryptographyHandler, IDevice device)
		{
			if (cryptographyHandler == null) { throw new ArgumentNullException(nameof(cryptographyHandler)); }
			if (device == null) { throw new ArgumentNullException(nameof(device)); }

			this.CryptographyHandler = cryptographyHandler;

			this.inputLink = new DataLink();
			this.inputLink.CommandReceived += this.OnInputLinkCommandReceived;
			this.inputLink.CorruptCommandReceived += this.OnInputLinkCorruptCommandReceived;
			this.inputLink.AbortRequested += this.OnInputLinkAbortRequested;

			this.outputLink = new DataLink();
			this.outputLink.CommandReceived += this.OnOutputLinkCommandReceived;
			this.outputLink.CorruptCommandReceived += this.OnOutputLinkCorruptCommandReceived;
			this.outputLink.AbortRequested += this.OnOutputLinkAbortRequested;

			this.device = device;
			this.device.Output += this.OnDeviceOutput;
		}

		public void Input(byte[] data)
		{
			this.inputLink.Input(data);
		}

		private void OnInputLinkCommandReceived(string command)
		{
			this.Output?.Invoke(new byte[] { ByteFlag.PACKET_ACKNOWLEDGE });
			this.device.Input(Checksum.Encapsulate(this.CryptographyHandler.Redo(command)).ToArray());
		}

		private void OnInputLinkCorruptCommandReceived(string command)
		{
			this.Output?.Invoke(new byte[] { ByteFlag.PACKET_NEGATIVE_ACKNOWLEDGE });
		}

		private void OnInputLinkAbortRequested()
		{
			this.Output?.Invoke(new byte[] { ByteFlag.ABORT_ACKNOWLEDGE });
			this.device.Input(ByteFlag.ABORT);
		}

		private void OnOutputLinkCommandReceived(string command)
		{
			this.device.Input(new byte[] { ByteFlag.PACKET_ACKNOWLEDGE });
			this.Output?.Invoke(Checksum.Encapsulate(this.CryptographyHandler.Undo(command)).ToArray());
		}

		private void OnOutputLinkCorruptCommandReceived(string command)
		{
			this.device.Input(ByteFlag.PACKET_NEGATIVE_ACKNOWLEDGE);
		}

		private void OnOutputLinkAbortRequested()
		{
			this.Output?.Invoke(new byte[] { ByteFlag.ABORT });
		}

		private void OnDeviceOutput(byte[] data)
		{
			this.outputLink.Input(data);
		}

		public void Dispose()
		{
			this.device.Dispose();
		}
	}
}

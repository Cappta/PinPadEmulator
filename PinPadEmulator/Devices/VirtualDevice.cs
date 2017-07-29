using PinPadEmulator.Crypto;
using System;
using System.Linq;

namespace PinPadEmulator.Devices
{
	public class VirtualDevice : IDevice
	{
		public event Action<string> CommandReceived;
		public event Action<string> CorruptCommand;
		public event Action AbortRequested;
		public event Action<byte[]> Output;

		public ICryptoHandler CryptographyHandler;

		private DataLink link;

		public VirtualDevice(ICryptoHandler cryptographyHandler)
		{
			if (cryptographyHandler == null) { throw new ArgumentNullException(nameof(cryptographyHandler)); }

			this.CryptographyHandler = cryptographyHandler;

			this.link = new DataLink();
			this.link.CommandReceived += this.OnLinkCommandReceived;
			this.link.CorruptCommandReceived += this.OnLinkCorruptCommandReceived;
			this.link.AbortRequested += this.OnLinkAbortRequested;
		}

		private void OnLinkCommandReceived(string command)
		{
			this.Reply(ByteFlag.PACKET_ACKNOWLEDGE);
			var cryptoHandled = this.CryptographyHandler.Handle(command);
			if (cryptoHandled != null)
			{
				this.Reply(Checksum.Encapsulate(cryptoHandled).ToArray());
				return;
			}
			this.CommandReceived?.Invoke(this.CryptographyHandler.Undo(command));
		}

		private void OnLinkCorruptCommandReceived(string command)
		{
			this.Reply(ByteFlag.PACKET_NEGATIVE_ACKNOWLEDGE);
			this.CorruptCommand?.Invoke(command);
		}

		private void OnLinkAbortRequested()
		{
			this.Reply(ByteFlag.ABORT_ACKNOWLEDGE);
			this.AbortRequested?.Invoke();
		}

		public void Input(byte[] data)
		{
			this.link.Input(data);
		}

		public void Reply(string response)
		{
			this.Reply(Checksum.Encapsulate(this.CryptographyHandler.Redo(response)).ToArray());
		}

		private void Reply(params byte[] data)
		{
			this.Output?.Invoke(data);
		}

		public void Dispose()
		{
		}
	}
}

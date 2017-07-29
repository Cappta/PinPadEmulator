using System;

namespace PinPadEmulator.Devices
{
	public interface IDevice : IDisposable
	{
		void Input(params byte[] data);
		event Action<byte[]> Output;
	}
}

namespace PinPadEmulator.Devices
{
	public class DeviceInputBuffer : DeviceBuffer
	{
		public DeviceInputBuffer(IDevice device)
			: base(device)
		{
		}

		public void Write(params byte[] data)
			=> this.Device.Input(data);
	}
}

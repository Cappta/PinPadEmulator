namespace PinPadEmulator.Devices
{
	public class DeviceInputBuffer : DeviceBuffer
	{
		public DeviceInputBuffer(IDevice device)
			: base(device)
		{
		}

		public void Write(params byte[] data)
		{
			lock (this.Buffer)
			{
				foreach (var item in data) { this.Buffer.Enqueue(item); }
			}
		}
	}
}

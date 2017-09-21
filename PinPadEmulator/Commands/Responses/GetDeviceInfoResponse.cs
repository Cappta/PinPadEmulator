using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetDeviceInfoResponse : BaseResponse
	{
		public override string Identifier => "GIN";

		public FixedLengthField<string> DeviceManufacturer { get; } = new FixedLengthField<string>(20);
		public FixedLengthField<string> DeviceModelAndCapacity { get; } = new FixedLengthField<string>(19);
		public FixedLengthField<string> ContaclessMode { get; } = new FixedLengthField<string>(1);
		public FixedLengthField<string> SoftwareAndFirmwareVersion { get; } = new FixedLengthField<string>(20);
		public FixedLengthField<string> SharedLibVersion { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<string> AppVersion { get; } = new FixedLengthField<string>(16);
		public FixedLengthField<string> SerialNumber { get; } = new FixedLengthField<string>(20);

	}
}

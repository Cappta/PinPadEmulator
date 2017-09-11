namespace PinPadEmulator
{
	internal class ByteFlag
	{
		public const byte PACKET_ACKNOWLEDGE = 0x06;
		public const byte PACKET_NEGATIVE_ACKNOWLEDGE = 0x15;
		public const byte PACKET_START = 0x16;
		public const byte PACKET_END = 0x17;

		public const byte ABORT_ACKNOWLEDGE = 0x04;
		public const byte ABORT = 0x18;
	}
}

using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GoOnChipResponse : BaseResponse
	{
		public override string Identifier => "GOC";

		public FixedLengthField<int> TransactionStatus { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<bool> ShouldRequestSignature { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<bool> PinWasValidatedOffline { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<int> InvalidOfflinePinValidations { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<bool> OfflinePinWasBlocked { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<bool> OnlinePinWasCaptured { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<byte[]> EncryptedPin { get; } = new FixedLengthField<byte[]>(16);
		public FixedLengthField<byte[]> KeySerialNumber { get; } = new FixedLengthField<byte[]>(20);
		public VariableLengthField<byte[]> EMVData { get; } = new VariableLengthField<byte[]>(3, 512);
		public VariableLengthField<int> AcquirerData { get; } = new VariableLengthField<int>(3);
	}
}

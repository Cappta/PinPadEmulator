using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Requests
{
	public class GoOnChipRequest : BaseRequest
	{
		public override string Identifier => "GOC";

		public FixedLengthField<int> AmountInCents { get; } = new FixedLengthField<int>(12);
		public FixedLengthField<int> CashbackInCents { get; } = new FixedLengthField<int>(12);
		public FixedLengthField<bool> PanIsInBlacklist { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<bool> TransactionMustBeOnline { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<bool> IsPinRequired { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<CryptoMethod> Method { get; } = new FixedLengthField<CryptoMethod>(1);
		public FixedLengthField<int> MasterKeyIndex { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> WorkingKey { get; } = new FixedLengthField<byte[]>(32);
		public FixedLengthField<bool> HasRiskManagement { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<byte[]> FloorLimit { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<int> TargetPercentage { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<byte[]> Treshould { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<int> MaxTargetPercentage { get; } = new FixedLengthField<int>(2);
		public VariableLengthField<string> AcquirerData { get; } = new VariableLengthField<string>(3, 32);

		public EmvTags RequiredTags { get; } = new EmvTags();
		public EmvTags OptionalTags { get; } = new EmvTags();
	}
}

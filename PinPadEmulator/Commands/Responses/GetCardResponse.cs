using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetCardResponse : BaseResponse
	{
		public override string Identifier => "GCR";

		public readonly FixedLengthField<CardType> CardType = new FixedLengthField<CardType>(2);
		public readonly FixedLengthField<ChipStatus> ChipStatus = new FixedLengthField<ChipStatus>(1);
		public readonly FixedLengthField<int> ApplicationType = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> AcquirerId = new FixedLengthField<int>(2);
		public readonly FixedLengthField<int> RecordIndex = new FixedLengthField<int>(2);
		public readonly PaddedVariableLengthField<string> Track1 = new PaddedVariableLengthField<string>(2, 76);
		public readonly PaddedVariableLengthField<string> Track2 = new PaddedVariableLengthField<string>(2, 37);
		public readonly PaddedVariableLengthField<string> Track3 = new PaddedVariableLengthField<string>(3, 104);
		public readonly PaddedVariableLengthField<string> Pan = new PaddedVariableLengthField<string>(2, 19);
		public readonly FixedLengthField<int> PanSequenceNumber = new FixedLengthField<int>(2);
		public readonly FixedLengthField<string> ApplicationLabel = new FixedLengthField<string>(16);
		public readonly FixedLengthField<int> ServiceCode = new FixedLengthField<int>(3);
		public readonly FixedLengthField<string> CardHolderName = new FixedLengthField<string>(26);
		public readonly OptionalDateTimeField CardExpirationDate = new OptionalDateTimeField("yyMMdd");
		public readonly PaddedVariableLengthField<string> ExternalCardNumber = new PaddedVariableLengthField<string>(2, 19);
		public readonly FixedLengthField<int> EletronicCoinAcceptorBalance = new FixedLengthField<int>(8);
		public readonly FixedLengthField<int> IssuerCountryCode = new FixedLengthField<int>(3);
		public readonly VariableLengthField<string> AcquirerData = new VariableLengthField<string>(3, 66);
	}
}

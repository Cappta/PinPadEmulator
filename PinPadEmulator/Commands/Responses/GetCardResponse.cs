using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands.Responses
{
	public class GetCardResponse : BaseResponse
	{
		public override string Identifier => "GCR";

		public FixedLengthField<CardType> CardType { get; } = new FixedLengthField<CardType>(2);
		public FixedLengthField<ChipStatus> ChipStatus { get; } = new FixedLengthField<ChipStatus>(1);
		public FixedLengthField<int> ApplicationType { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> RecordIndex { get; } = new FixedLengthField<int>(2);
		public PaddedVariableLengthField<string> Track1 { get; } = new PaddedVariableLengthField<string>(2, 76);
		public PaddedVariableLengthField<string> Track2 { get; } = new PaddedVariableLengthField<string>(2, 37);
		public PaddedVariableLengthField<string> Track3 { get; } = new PaddedVariableLengthField<string>(3, 104);
		public PaddedVariableLengthField<string> Pan { get; } = new PaddedVariableLengthField<string>(2, 19);
		public FixedLengthField<int> PanSequenceNumber { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> ApplicationLabel { get; } = new FixedLengthField<string>(16);
		public FixedLengthField<int> ServiceCode { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<string> CardHolderName { get; } = new FixedLengthField<string>(26);
		public DateTimeField CardExpirationDate { get; } = new DateTimeField("yyMMdd").Optional();

		// Esse cara está ofuscado como GCR_RUF1 no documento da v199, além de ter o tamanho errado (Reportado como 39, mas é 29)
		public PaddedVariableLengthField<string> ExternalCardNumber { get; } = new PaddedVariableLengthField<string>(2, 19);
		public FixedLengthField<int> EletronicCoinAcceptorBalance { get; } = new FixedLengthField<int>(8);

		public FixedLengthField<int> IssuerCountryCode { get; } = new FixedLengthField<int>(3);
		public VariableLengthField<string> AcquirerData { get; } = new VariableLengthField<string>(3, 66);
	}
}

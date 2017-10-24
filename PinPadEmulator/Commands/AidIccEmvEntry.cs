using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class AidIccEmvEntry : FieldGroup
	{
		public FixedValueField<int> TableId { get; } = new FixedValueField<int>(1, new FixedLengthField<int>(1));
		public FixedLengthField<int> AcquirerId { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<int> EntryId { get; } = new FixedLengthField<int>(2);
		public PaddedVariableLengthField<byte[]> AID { get; } = new PaddedVariableLengthField<byte[]>(2, 32);
		public FixedLengthField<int> ApplicationType { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> ApplicationLabel { get; } = new FixedLengthField<string>(16);
		public FixedValueField<ApplicationPattern> TableApplicationPattern { get; } = new FixedValueField<ApplicationPattern>(ApplicationPattern.IccEmv, new FixedLengthField<ApplicationPattern>(2));
		public FixedLengthField<byte[]> AppVersionOption1 { get; } = new FixedLengthField<byte[]>(4);
		public FixedLengthField<byte[]> AppVersionOption2 { get; } = new FixedLengthField<byte[]>(4);
		public FixedLengthField<byte[]> AppVersionOption3 { get; } = new FixedLengthField<byte[]>(4);
		public FixedLengthField<int> TerminalCountryCode { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TransactionCurrencyCode { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TransactionCurrencyExponent { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<string> MerchantIdentifier { get; } = new FixedLengthField<string>(15);
		public FixedLengthField<string> MerchantCategoryCode { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<string> TerminalIdentification { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<byte[]> TerminalCababilities { get; } = new FixedLengthField<byte[]>(6);
		public FixedLengthField<byte[]> AditionalTerminalCababilities { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<TerminalType> TerminalType { get; } = new FixedLengthField<TerminalType>(2);
		public FixedLengthField<byte[]> DefaultTerminalActionCode { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<byte[]> DenialTerminalActionCode { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<byte[]> OnlineTerminalActionCode { get; } = new FixedLengthField<byte[]>(10);
		public FixedLengthField<byte[]> TerminalFloorLimit { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<string> TransactionCategoryCode { get; } = new FixedLengthField<string>(1);
		public FixedLengthField<bool> ContactlessSupported { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<ContactlessCapability> ContactlessTerminalCapability { get; } = new FixedLengthField<ContactlessCapability>(1);
		public FixedLengthField<byte[]> TerminalContactlessTransactionLimit { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<byte[]> TerminalContactlessFloorLimit { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<byte[]> TerminalCVMRequiredLimit { get; } = new FixedLengthField<byte[]>(8);
		public FixedLengthField<byte[]> PayPassMagStripeAppVersionNumber { get; } = new FixedLengthField<byte[]>(4);
		public FixedLengthField<bool> AllowAppSelection { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<byte[]> DefaultTransactionCertificateObjectList { get; } = new FixedLengthField<byte[]>(40);
		public FixedLengthField<byte[]> DefaultDynamicAuthenticationObjectList { get; } = new FixedLengthField<byte[]>(40);
		public FixedLengthField<string> AuthorizationResponseCodes { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<byte[]> ContactlessDefaultTerminalActionCode { get; } = new FixedLengthField<byte[]>(10).Optional();
		public FixedLengthField<byte[]> ContactlessDenialTerminalActionCode { get; } = new FixedLengthField<byte[]>(10).Optional();
		public FixedLengthField<byte[]> ContactlessOnlineTerminalActionCode { get; } = new FixedLengthField<byte[]>(10).Optional();
	}
}

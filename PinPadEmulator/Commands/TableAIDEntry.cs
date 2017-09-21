using PinPadEmulator.Fields;

namespace PinPadEmulator.Commands
{
	public class TableAIDEntry : TableEntry
	{
		public PaddedVariableLengthField<string> AID { get; } = new PaddedVariableLengthField<string>(2, 32);
		public FixedLengthField<int> ApplicationType { get; } = new FixedLengthField<int>(2);
		public FixedLengthField<string> ApplicationLabel { get; } = new FixedLengthField<string>(16);
		public FixedLengthField<ApplicationPattern> ApplicationPattern { get; } = new FixedLengthField<ApplicationPattern>(2);
		public FixedLengthField<string> AppVersionOption1 { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<string> AppVersionOption2 { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<string> AppVersionOption3 { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<int> TerminalCountryCode { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TransactionCurrencyCode { get; } = new FixedLengthField<int>(3);
		public FixedLengthField<int> TransactionCurrencyExponent { get; } = new FixedLengthField<int>(1);
		public FixedLengthField<string> MerchantIdentifier { get; } = new FixedLengthField<string>(15);
		public FixedLengthField<int> MerchantCategoryCode { get; } = new FixedLengthField<int>(4);
		public FixedLengthField<string> TerminalIdentification { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> TerminalCababilities { get; } = new FixedLengthField<string>(6);
		public FixedLengthField<string> AditionalTerminalCababilities { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<TerminalType> TermianlType { get; } = new FixedLengthField<TerminalType>(2);
		public FixedLengthField<string> DefaultTerminalActionCode { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> DenialTerminalActionCode { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> OnlineTerminalActionCode { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> TerminalForLimit { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> TransactionCategoryCode { get; } = new FixedLengthField<string>(1);
		public FixedLengthField<bool> ContactlessSupported { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<ContactlessCapability> ContactlessTerminalCapability { get; } = new FixedLengthField<ContactlessCapability>(1);
		public FixedLengthField<string> TermianalContactlessTransactionLimit { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> TermianalContactlessFloorLimit { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> TermianalCVMRequiredLimit { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> PayPassMagStripeAppVersionNumber { get; } = new FixedLengthField<string>(4);
		public FixedLengthField<bool> AllowAppSelection { get; } = new FixedLengthField<bool>(1);
		public FixedLengthField<string> DefaultTransactionCertificateObjectList { get; } = new FixedLengthField<string>(40);
		public FixedLengthField<string> DefaultDynamicAuthenticationObjectList { get; } = new FixedLengthField<string>(40);
		public FixedLengthField<string> AuthorizationResponseCodes { get; } = new FixedLengthField<string>(8);
		public FixedLengthField<string> ContactlessDefaultTerminalActionCode { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> ContactlessDenialTerminalActionCode { get; } = new FixedLengthField<string>(10);
		public FixedLengthField<string> ContactlessOnlineTerminalActionCode { get; } = new FixedLengthField<string>(10);
	}
}

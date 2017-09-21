namespace PinPadEmulator.Commands
{
	public enum ContactlessCapability
	{
		Unsupported = 0,

		VisaMsd = 1,

		VisaQVSDC = 2,

		MasterCardPayPassMagStripe = 3,

		MasterCardPayPassMagStripeAndChip = 4,

		AmexExpresspayMagStripe = 5,

		AmexExpresspayEMV = 6
	}
}

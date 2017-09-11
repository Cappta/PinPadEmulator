namespace PinPadEmulator.Commands
{
	public enum CryptoMethod
	{
		MasterKeyWorkingKeyDES = 0,

		MasterKeyWorkingKeyTDES = 1,

		DerivedUniqueKeyPerTransactionDES = 2,

		DerivedUniqueKeyPerTransactionTDES = 3,
	}
}

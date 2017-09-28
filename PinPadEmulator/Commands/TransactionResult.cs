namespace PinPadEmulator.Commands
{
	public enum TransactionResult
	{
		Approved = 0,

		DeniedByCard = 1,

		DeniedByAcquirer = 2
	}
}

namespace PinPadEmulator.Crypto
{
	public interface ICryptoHandler
	{
		string Undo(string command);
		string Redo(string command);
		string Handle(string command);
	}
}

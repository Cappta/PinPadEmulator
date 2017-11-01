using PinPadSDK.Commands.Responses;

namespace PinPadEmulator.Crypto
{
	public interface ICryptoHandler
	{
		string Undo(string command);
		string Redo(string command);
		BaseResponse Handle(string command);
	}
}

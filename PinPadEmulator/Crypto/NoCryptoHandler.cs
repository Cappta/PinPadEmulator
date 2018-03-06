using PinPadSDK.Commands.Responses;

namespace PinPadEmulator.Crypto
{
	public class NoCryptoHandler : ICryptoHandler
	{
		public BaseResponse Handle(string command)
		{
			return null;
		}

		public string Redo(string command)
		{
			return command;
		}

		public string Undo(string command)
		{
			return command;
		}
	}
}

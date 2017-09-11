using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class GetPinRequestTests
	{
		[TestMethod]
		public void TestGetPinRequest()
		{
			var getPinRequest = new GetPinRequest();
			getPinRequest.Init(new StringReader("GPN0932010000000000000000000000000000000019636AECB5EDB2884C82910412VALOR 1,59      SENHA:          "));

			Assert.AreEqual(CryptoMethod.DerivedUniqueKeyPerTransactionDES, getPinRequest.Method.Value);
		}
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands.Responses;

namespace PinPadEmulatorTests.Responses
{
	[TestClass]
	public class GetInfoResponseTests
	{
		[TestMethod]
		public void Batata()
		{
			var response = new GetAcquirerInfoResponse();
			response.Status.Value = 11;

			var test = response.ToString();

			Assert.AreEqual("GIN011", test);
		}
	}
}

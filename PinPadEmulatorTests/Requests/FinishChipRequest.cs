using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class FinishChipRequestTests
	{
		[TestMethod]
		public void TestGetCardRequest()
		{
			var request = new FinishChipRequest();
			request.Init(new StringReader("FNC0340000012910A1A1B1C1D1E1F2A2B0000000021009959F379F279F269F10"));

			Assert.AreEqual(false, request.Success.Value);
			Assert.AreEqual(IssuerType.FullGrade, request.IssuerType.Value);
			Assert.AreEqual("00", request.AuthorizationResponseCode.Value);
			Assert.AreEqual("910A1A1B1C1D", request.EMVData.ToString());
		}
	}
}

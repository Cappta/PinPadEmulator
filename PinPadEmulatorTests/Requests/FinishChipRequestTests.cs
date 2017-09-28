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
		public void TestFinishChipRequest()
		{
			var request = new FinishChipRequest();
			request.Init(new StringReader("FNC0340000012910A1A1B1C1D1E1F2A2B0000000021009959F379F279F269F10"));

			Assert.AreEqual(0, request.Success.Value);
			Assert.AreEqual(IssuerType.FullGrade, request.IssuerType.Value);
			Assert.AreEqual("00", request.AuthorizationResponseCode.Value);
			Assert.AreEqual("012910A1A1B1C1D1E1F2A2B0000", request.EMVData.ToString());
			Assert.AreEqual(0, request.AcquirerData.Value);
			Assert.AreEqual("021009959F379F279F269F10", request.Tags.ToString());
		}
	}
}

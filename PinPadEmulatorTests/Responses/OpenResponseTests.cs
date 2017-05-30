using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands.Responses;

namespace PinPadEmulatorTests.Responses
{
	[TestClass]
	public class OpenResponseTests
	{
		[TestMethod]
		public void TestOpenResponse()
		{
			var openResponse = new OpenResponse();

			var command = openResponse.ToString();
			var expected = "OPN000";
			Assert.AreEqual(expected, command);
		}
	}
}

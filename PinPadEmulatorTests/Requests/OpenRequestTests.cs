using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class OpenRequestTests
	{
		[TestMethod]
		public void TestOpenRequest()
		{
			var openRequest = new OpenRequest();
			openRequest.Init(new StringReader("OPN"));
		}
	}
}

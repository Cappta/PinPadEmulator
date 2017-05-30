using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class GetCardRequestTests
	{
		[TestMethod]
		public void TestGetCardRequest()
		{
			var getCardRequest = new GetCardRequest();
			getCardRequest.Init(new StringReader("GCR0520000000000001000131207102355876523456803021402220217"));

			Assert.AreEqual(0, getCardRequest.AcquirerId.Value);
			Assert.AreEqual(0, getCardRequest.TargetAid.Value);
			Assert.AreEqual(1000, getCardRequest.TransactionAmount.Value);
			Assert.AreEqual("131207102355", getCardRequest.TransactionDateTime.Value.ToString("yyMMddhhmmss"));
			Assert.AreEqual("8765234568", getCardRequest.TableVersion.Value);
			Assert.AreEqual(3, getCardRequest.AidEntryReferences.Count);

			Assert.AreEqual(2, getCardRequest.AidEntryReferences[0].AcquirerId.Value);
			Assert.AreEqual(14, getCardRequest.AidEntryReferences[0].RecordIndex.Value);

			Assert.AreEqual(2, getCardRequest.AidEntryReferences[1].AcquirerId.Value);
			Assert.AreEqual(22, getCardRequest.AidEntryReferences[1].RecordIndex.Value);

			Assert.AreEqual(2, getCardRequest.AidEntryReferences[2].AcquirerId.Value);
			Assert.AreEqual(17, getCardRequest.AidEntryReferences[2].RecordIndex.Value);

			Assert.IsNull(getCardRequest.ContactlessOn.Value);
		}
	}
}

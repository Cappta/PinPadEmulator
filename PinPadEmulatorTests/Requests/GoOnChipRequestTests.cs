using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class GoOnChipRequestTests
	{
		[TestMethod]
		public void TestGoOnChipRequest()
		{
			var request = new GoOnChipRequest();
			request.Init(new StringReader("GOC086000000000010000000000000011316000000000000000000000000000000001000000000000000000000000590285F2A82959A9C9F029F109F1A9F1E9F269F339F369F379F279F345F34003000"));
			
			Assert.AreEqual(10, request.AmountInCents.Value);
			Assert.AreEqual(0, request.CashbackInCents.Value);
			Assert.AreEqual(false, request.PanIsInBlacklist.Value);
			Assert.AreEqual(true, request.TransactionMustBeOnline.Value);
			Assert.AreEqual(true, request.IsPinRequired.Value);
			Assert.AreEqual(CryptoMethod.DerivedUniqueKeyPerTransactionTDES, request.Method.Value);
			Assert.AreEqual(16, request.MasterKeyIndex.Value);
			Assert.AreEqual(new string('0', 32), request.WorkingKey.ToString());
			Assert.AreEqual(true, request.HasRiskManagement.Value);
			Assert.AreEqual(new string('0', 8), request.FloorLimit.ToString());
			Assert.AreEqual(0, request.TargetPercentage.Value);
			Assert.AreEqual(new string('0', 8), request.Treshould.ToString());
			Assert.AreEqual(0, request.MaxTargetPercentage.Value);
			Assert.AreEqual(null, request.AcquirerData.Value);
			Assert.AreEqual("0590285F2A82959A9C9F029F109F1A9F1E9F269F339F369F379F279F345F34", request.RequiredTags.ToString());
			Assert.AreEqual("003000", request.OptionalTags.ToString());
		}
	}
}

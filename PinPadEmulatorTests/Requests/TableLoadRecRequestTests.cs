using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Utils;

namespace PinPadEmulatorTests.Requests
{
	[TestClass]
	public class TableLoadRecRequestTests
	{
		[TestMethod]
		public void TestTableLoadRecRequest_with_an_AIDEntry()
		{
			var request = new TableLoadRecRequest();
			request.Init(new StringReader("TLR286012841020107A000000004101000000000000000000001REDECARD CREDITO030002000200020769862074609066800773    07460906E0F0C8FF80B0F00122FC50ACA0000000000000FC50ACF80000000014R00000000000000000000000000000009F02065F2A029A039C0195059F3704FFFFFFFFFF9F3704FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFY1Z1Y3Z3"));

			Assert.AreEqual(1, request.Entries.Count);
			Assert.AreEqual(284, ((TableAIDEntry)request.Entries[0]).Length.Value);
			Assert.AreEqual(1, ((TableAIDEntry)request.Entries[0]).TableId.Value);
			Assert.AreEqual(2, ((TableAIDEntry)request.Entries[0]).AcquirerId.Value);
			Assert.AreEqual("07A0000000041010000000000000000000", ((TableAIDEntry)request.Entries[0]).AID.ToString());
			Assert.AreEqual(1, ((TableAIDEntry)request.Entries[0]).ApplicationType.Value);
			Assert.AreEqual("REDECARD CREDITO", ((TableAIDEntry)request.Entries[0]).ApplicationLabel.Value);
			Assert.AreEqual(ApplicationPattern.IccEmv, ((TableAIDEntry)request.Entries[0]).ApplicationPattern.Value);
			Assert.AreEqual("0002", ((TableAIDEntry)request.Entries[0]).AppVersionOption1.ToString());
			Assert.AreEqual("0002", ((TableAIDEntry)request.Entries[0]).AppVersionOption2.ToString());
			Assert.AreEqual("0002", ((TableAIDEntry)request.Entries[0]).AppVersionOption3.ToString());
			Assert.AreEqual(76, ((TableAIDEntry)request.Entries[0]).TerminalCountryCode.Value);
			Assert.AreEqual(986, ((TableAIDEntry)request.Entries[0]).TransactionCurrencyCode.Value);
			Assert.AreEqual(2, ((TableAIDEntry)request.Entries[0]).TransactionCurrencyExponent.Value);
			Assert.AreEqual("074609066800773", ((TableAIDEntry)request.Entries[0]).MerchantIdentifier.Value);
			Assert.AreEqual(null, ((TableAIDEntry)request.Entries[0]).MerchantCategoryCode.Value);
			Assert.AreEqual("07460906", ((TableAIDEntry)request.Entries[0]).TerminalIdentification.Value);
			Assert.AreEqual("E0F0C8", ((TableAIDEntry)request.Entries[0]).TerminalCababilities.ToString());
			Assert.AreEqual("FF80B0F001", ((TableAIDEntry)request.Entries[0]).AditionalTerminalCababilities.ToString());
			Assert.AreEqual(TerminalType.OfflineWithOnlineCapabilities, ((TableAIDEntry)request.Entries[0]).TerminalType.Value);
			Assert.AreEqual("FC50ACA000", ((TableAIDEntry)request.Entries[0]).DefaultTerminalActionCode.ToString());
			Assert.AreEqual("0000000000", ((TableAIDEntry)request.Entries[0]).DenialTerminalActionCode.ToString());
			Assert.AreEqual("FC50ACF800", ((TableAIDEntry)request.Entries[0]).OnlineTerminalActionCode.ToString());
			Assert.AreEqual("00000014", ((TableAIDEntry)request.Entries[0]).TerminalFloorLimit.ToString());
			Assert.AreEqual("R", ((TableAIDEntry)request.Entries[0]).TransactionCategoryCode.Value);
			Assert.AreEqual(false, ((TableAIDEntry)request.Entries[0]).ContactlessSupported.Value);
			Assert.AreEqual(ContactlessCapability.Unsupported, ((TableAIDEntry)request.Entries[0]).ContactlessTerminalCapability.Value);
			Assert.AreEqual("00000000", ((TableAIDEntry)request.Entries[0]).TerminalContactlessTransactionLimit.ToString());
			Assert.AreEqual("00000000", ((TableAIDEntry)request.Entries[0]).TerminalContactlessFloorLimit.ToString());
			Assert.AreEqual("00000000", ((TableAIDEntry)request.Entries[0]).TerminalCVMRequiredLimit.ToString());
			Assert.AreEqual("0000", ((TableAIDEntry)request.Entries[0]).PayPassMagStripeAppVersionNumber.ToString());
			Assert.AreEqual(false, ((TableAIDEntry)request.Entries[0]).AllowAppSelection.Value);
			Assert.AreEqual("9F02065F2A029A039C0195059F3704FFFFFFFFFF", ((TableAIDEntry)request.Entries[0]).DefaultTransactionCertificateObjectList.ToString());
			Assert.AreEqual("9F3704FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF", ((TableAIDEntry)request.Entries[0]).DefaultDynamicAuthenticationObjectList.ToString());
			Assert.AreEqual("Y1Z1Y3Z3", ((TableAIDEntry)request.Entries[0]).AuthorizationResponseCodes.Value);
		}

        [TestMethod]
        public void TestTableLoadRecRequest_with_an_CAPKEntry()
        {
            var request = new TableLoadRecRequest();
            request.Init(new StringReader("TLR6130161120201A00000000351001030000144BE9E1FA5E9A803852999C4AB432DB28600DCD9DAB76DFAAA47355A0FE37B1508AC6BF38860D3C6C2E5B12A3CAAF2A7005A7241EBAA7771112C74CF9A0634652FBCA0E5980C54A64761EA101A114E0F0B5572ADD57D010B7C9C887E104CA4EE1272DA66D997B9A90B5A6D624AB6C57E73C8F919000EB5F684898EF8C3DBEFB330C62660BED88EA78E909AFF05F6DA627B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001EE1511CEC71020A9B90443B37B1D5F6E703030F6FFFFFFFF0000000000000000000000000000000000"));

            Assert.AreEqual(1, request.Entries.Count);
            Assert.AreEqual(611, ((TableCAPKEntry)request.Entries[0]).Length.Value);
            Assert.AreEqual(2, ((TableCAPKEntry)request.Entries[0]).TableId.Value);
            Assert.AreEqual(2, ((TableCAPKEntry)request.Entries[0]).AcquirerId.Value);
            Assert.AreEqual(1, ((TableCAPKEntry)request.Entries[0]).EntryId.Value);
            Assert.AreEqual("A000000003", ((TableCAPKEntry)request.Entries[0]).RID.ToString());
            Assert.AreEqual("51", ((TableCAPKEntry)request.Entries[0]).CAPKId.ToString());
            Assert.AreEqual("00", ((TableCAPKEntry)request.Entries[0]).Reserved.Value);
            Assert.AreEqual("1030000", ((TableCAPKEntry)request.Entries[0]).PublicKeyExponent.ToString());
            Assert.AreEqual("144BE9E1FA5E9A803852999C4AB432DB28600DCD9DAB76DFAAA47355A0FE37B1508AC6BF38860D3C6C2E5B12A3CAAF2A7005A7241EBAA7771112C74CF9A0634652FBCA0E5980C54A64761EA101A114E0F0B5572ADD57D010B7C9C887E104CA4EE1272DA66D997B9A90B5A6D624AB6C57E73C8F919000EB5F684898EF8C3DBEFB330C62660BED88EA78E909AFF05F6DA627B0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", ((TableCAPKEntry)request.Entries[0]).PublicKeyModulus.ToString());
            Assert.AreEqual(true, ((TableCAPKEntry)request.Entries[0]).HasChecksum.Value);
            Assert.AreEqual("EE1511CEC71020A9B90443B37B1D5F6E703030F6", ((TableCAPKEntry)request.Entries[0]).PublicKeChecksum.ToString());
            Assert.AreEqual("FFFFFFFF0000000000000000000000000000000000", ((TableCAPKEntry)request.Entries[0]).Reserved2.Value);
        }

        [TestMethod]
        public void TestTableLoadRecRequest_with_an_RevokedCertificateEntry()
        {
            var request = new TableLoadRecRequest();
            request.Init(new StringReader("TLR6130161120201A00000000351001030000144BE9E1FA5E9A803852999C4AB432DB28600DCD9DAB76DFAAA47355A0FE37B1508AC6BF38860D3C6C2E5B12A3CAAF2A7005A7241EBAA7771112C74CF9A0634652FBCA0E5980C54A64761EA101A114E0F0B5572ADD57D010B7C9C887E104CA4EE1272DA66D997B9A90B5A6D624AB6C57E73C8F919000EB5F684898EF8C3DBEFB330C62660BED88EA78E909AFF05F6DA627B00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001EE1511CEC71020A9B90443B37B1D5F6E703030F6FFFFFFFF0000000000000000000000000000000000"));

            Assert.AreEqual(1, request.Entries.Count);
            Assert.AreEqual(611, ((TableCAPKEntry)request.Entries[0]).Length.Value);
            Assert.AreEqual(2, ((TableCAPKEntry)request.Entries[0]).TableId.Value);
            Assert.AreEqual(2, ((TableCAPKEntry)request.Entries[0]).AcquirerId.Value);
            Assert.AreEqual(1, ((TableCAPKEntry)request.Entries[0]).EntryId.Value);
            Assert.AreEqual("A000000003", ((TableCAPKEntry)request.Entries[0]).RID.ToString());
            Assert.AreEqual("51", ((TableCAPKEntry)request.Entries[0]).CAPKId.ToString());
            Assert.AreEqual("00", ((TableCAPKEntry)request.Entries[0]).Reserved.Value);
            Assert.AreEqual("1030000", ((TableCAPKEntry)request.Entries[0]).PublicKeyExponent.ToString());
            Assert.AreEqual("144BE9E1FA5E9A803852999C4AB432DB28600DCD9DAB76DFAAA47355A0FE37B1508AC6BF38860D3C6C2E5B12A3CAAF2A7005A7241EBAA7771112C74CF9A0634652FBCA0E5980C54A64761EA101A114E0F0B5572ADD57D010B7C9C887E104CA4EE1272DA66D997B9A90B5A6D624AB6C57E73C8F919000EB5F684898EF8C3DBEFB330C62660BED88EA78E909AFF05F6DA627B0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", ((TableCAPKEntry)request.Entries[0]).PublicKeyModulus.ToString());
            Assert.AreEqual(true, ((TableCAPKEntry)request.Entries[0]).HasChecksum.Value);
            Assert.AreEqual("EE1511CEC71020A9B90443B37B1D5F6E703030F6", ((TableCAPKEntry)request.Entries[0]).PublicKeChecksum.ToString());
            Assert.AreEqual("FFFFFFFF0000000000000000000000000000000000", ((TableCAPKEntry)request.Entries[0]).Reserved2.Value);
        }
    }
}

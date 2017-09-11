using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Responses;
using System;

namespace PinPadEmulatorTests.Responses
{
	[TestClass]
	public class GetCardResponseTests
	{
		[TestMethod]
		public void TestGetCardResponse()
		{
			var getCardResponse = new GetCardResponse();

			getCardResponse.CardType.Value = CardType.IccEmv;
			getCardResponse.ApplicationType.Value = 01;
			getCardResponse.AcquirerId.Value = 02;
			getCardResponse.RecordIndex.Value = 22;
			getCardResponse.Track2.Value = "5464999910769990=19122062812366907700";
			getCardResponse.Pan.Value = "5464999910769990";
			getCardResponse.ApplicationLabel.Value = "CREDITO";
			getCardResponse.CardHolderName.Value = "LEE WEINRIB / GEDDY";
			getCardResponse.CardExpirationDate.Value = new DateTime(2019, 12, 31);
			getCardResponse.IssuerCountryCode.Value = 076;
			getCardResponse.AcquirerData.Value = "9F0702FF00";

			var command = getCardResponse.ToString();
			var expected = "GCR00035203001022200                                                                            375464999910769990=19122062812366907700000                                                                                                        165464999910769990   00CREDITO         000LEE WEINRIB / GEDDY       19123100                   000000000760109F0702FF00";
			Assert.AreEqual(expected, command);
		}
	}
}

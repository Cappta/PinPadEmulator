using Microsoft.VisualStudio.TestTools.UnitTesting;
using PinPadEmulator;
using PinPadEmulator.Commands.Requests;
using System.Text;

namespace PinPadEmulatorTests
{
	[TestClass]
	public class EmulatorTests
	{
		Emulator emulator;

		[TestInitialize]
		public void Initialize()
		{
			this.emulator = new Emulator();
		}

		[TestMethod]
		public void When_receiving_OPN_should_route_OpenRequest()
		{
			var receivedRequest = default(BaseRequest);
			this.emulator.Handle<OpenRequest>(request => receivedRequest = request);

			this.emulator.Input(0x16);
			this.emulator.Input(Encoding.ASCII.GetBytes("OPN"));
			this.emulator.Input(0x17, 0xA8, 0xA9);

			Assert.IsNotNull(receivedRequest);
		}

		[TestMethod]
		public void When_receiving_unknown_request_should_invoke_unknown_request()
		{
			var expectedCommand = "UNK";

			var receivedCommand = default(string);
			this.emulator.UnknownRequest += (command) => { receivedCommand = command; };

			this.emulator.Input(0x16);
			this.emulator.Input(Encoding.ASCII.GetBytes(expectedCommand));
			this.emulator.Input(0x17, 0x7C, 0x32);

			Assert.AreEqual(expectedCommand, receivedCommand);
		}

		[TestMethod]
		public void When_receiving_corrupt_OPN_should_invoke_corrupt_request()
		{
			var expectedCommand = "OPM";

			var receivedCommand = default(string);
			this.emulator.CorruptRequest += (command) => { receivedCommand = command; };

			this.emulator.Input(0x16);
			this.emulator.Input(Encoding.ASCII.GetBytes(expectedCommand));
			this.emulator.Input(0x17, 0xA8, 0xA9);

			Assert.AreEqual(expectedCommand, receivedCommand);
		}

		[TestMethod]
		public void When_not_handling_a_request_should_invoke_unhandled_request()
		{
			var unhandledRequest = default(BaseRequest);
			this.emulator.UnhandledRequest += (request) => { unhandledRequest = request; };

			this.emulator.Input(0x16);
			this.emulator.Input(Encoding.ASCII.GetBytes("OPN"));
			this.emulator.Input(0x17, 0xA8, 0xA9);

			Assert.IsNotNull(unhandledRequest);
		}
	}
}

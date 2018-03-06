using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PinPadEmulator.Crypto;
using PinPadSDK.Commands.Requests;
using PinPadSDK.Devices;
using System;
using System.Text;

namespace PinPadEmulator.Tests
{
	[TestClass]
	public class EmulatorTests
	{
		private IDevice device;

		private ICryptoHandler cryptoHandler;

		private Emulator emulator;

		[TestInitialize]
		public void Initialize()
		{
			this.device = Substitute.For<IDevice>();
			this.cryptoHandler = Substitute.For<ICryptoHandler>();
			this.emulator = new Emulator(this.device, this.cryptoHandler);

			this.cryptoHandler.Undo(Arg.Any<string>()).Returns((callInfo) => callInfo.Arg<string>());

			this.device.When(d => d.Input(Arg.Any<byte[]>())).Do(data =>
			{
				this.device.Output += Raise.Event<Action<byte[]>>(data.Arg<byte[]>());
			});
		}

		[TestMethod]
		public void When_receiving_OPN_should_route_OpenRequest()
		{
			var receivedRequest = default(BaseRequest);
			this.emulator.Handle<OpenRequest>(request => receivedRequest = request);

			this.device.Input(0x16);
			this.device.Input(Encoding.ASCII.GetBytes("OPN"));
			this.device.Input(0x17, 0xA8, 0xA9);

			Assert.IsNotNull(receivedRequest);
		}

		[TestMethod]
		public void When_receiving_unknown_request_should_invoke_unknown_request()
		{
			var expectedCommand = "UNK";

			var receivedCommand = default(string);
			this.emulator.UnknownRequest += (command) => { receivedCommand = command; };

			this.device.Input(0x16);
			this.device.Input(Encoding.ASCII.GetBytes(expectedCommand));
			this.device.Input(0x17, 0x7C, 0x32);

			Assert.AreEqual(expectedCommand, receivedCommand);
		}

		[TestMethod]
		public void When_receiving_corrupt_OPN_should_invoke_corrupt_request()
		{
			var expectedCommand = "OPM";

			var receivedCommand = default(string);
			this.emulator.CorruptRequest += (command) => { receivedCommand = command; };

			this.device.Input(0x16);
			this.device.Input(Encoding.ASCII.GetBytes(expectedCommand));
			this.device.Input(0x17, 0xA8, 0xA9);

			Assert.AreEqual(expectedCommand, receivedCommand);
		}

		[TestMethod]
		public void When_not_handling_a_request_should_invoke_unhandled_request()
		{
			var unhandledRequest = default(BaseRequest);
			this.emulator.UnhandledRequest += (request) => { unhandledRequest = request; };

			this.device.Input(0x16);
			this.device.Input(Encoding.ASCII.GetBytes("OPN"));
			this.device.Input(0x17, 0xA8, 0xA9);

			Assert.IsNotNull(unhandledRequest);
		}
	}
}

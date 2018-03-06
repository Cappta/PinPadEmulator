using PinPadEmulator.Crypto;
using PinPadSDK;
using PinPadSDK.Commands.Requests;
using PinPadSDK.Commands.Responses;
using PinPadSDK.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PinPadEmulator
{
	public class Emulator
	{
		public event Action<string> CorruptRequest;
		public event Action<string> UnknownRequest;
		public event Action<BaseRequest> UnhandledRequest;
		public event Action AbortRequested;

		private readonly DataLink dataLink;
		private readonly Deserializer<BaseRequest> deserializer;
		private readonly IDevice device;
		private readonly ICryptoHandler cryptoHandler;

		private int responseCounter = 0;

		private Dictionary<Type, Action<BaseRequest>> typeHandlerDictionary = new Dictionary<Type, Action<BaseRequest>>();

		public Emulator(IDevice device, ICryptoHandler cryptoHandler)
		{
			this.device = device ?? throw new ArgumentNullException(nameof(device));
			this.cryptoHandler = cryptoHandler ?? throw new ArgumentNullException(nameof(cryptoHandler));

			this.device = device;
			this.device.Output += this.OnDeviceOutput;

			this.dataLink = new DataLink();
			this.deserializer = new Deserializer<BaseRequest>();

			this.dataLink.CommandReceived += this.OnCommandReceived;
			this.dataLink.CorruptCommandReceived += this.OnCorruptCommandReceived;
			this.dataLink.AbortRequested += this.OnAbortRequested;
		}

		private void OnCommandReceived(string command)
		{
			this.DeviceInput(ByteFlag.PACKET_ACKNOWLEDGE);

			Debug.WriteLine($"REQUEST: {command}");

			var cryptoHandled = this.cryptoHandler.Handle(command);
			if (cryptoHandled != null)
			{
				this.DeviceInput(Checksum.Encapsulate(cryptoHandled.ToString()).ToArray());
				return;
			}

			var request = this.deserializer.Deserialize(this.cryptoHandler.Undo(command));
			if (request == null) { this.UnknownRequest?.Invoke(command); return; }

			this.RouteRequest(request);
		}

		private void OnCorruptCommandReceived(string command)
		{
			this.DeviceInput(ByteFlag.PACKET_NEGATIVE_ACKNOWLEDGE);

			this.CorruptRequest?.Invoke(command);
		}

		private void OnAbortRequested()
		{
			this.DeviceInput(ByteFlag.ABORT_ACKNOWLEDGE);

			this.AbortRequested?.Invoke();
		}

		public void Handle(Action<BaseRequest> handler)
		{
			this.typeHandlerDictionary.Add(typeof(BaseRequest), handler);
		}

		public void Handle<requestType>(Action<requestType> handler) where requestType : BaseRequest
		{
			this.typeHandlerDictionary.Add(typeof(requestType), request => handler((requestType)request));
		}

		private void RouteRequest(BaseRequest request)
		{
			var initialResponseCounter = this.responseCounter;

			var typeOfRequest = request.GetType();
			var handlers = this.typeHandlerDictionary.Where(kvp => kvp.Key.IsAssignableFrom(typeOfRequest)).Select(kvp => kvp.Value);

			var taskCollection = new List<Task>();

			foreach (var handler in handlers)
			{
				taskCollection.Add(Task.Factory.StartNew(() => { handler(request); }));
			}

			Task.WaitAll(taskCollection.ToArray());

			if (initialResponseCounter < this.responseCounter) { return; }

			this.UnhandledRequest?.Invoke(request);
		}

		public void Respond(BaseResponse response)
		{
			this.responseCounter++;

			var command = response.ToString();
			var redone = this.cryptoHandler.Redo(command);
			Debug.WriteLine($"RESPONSE: {command} : {redone}");

			this.DeviceInput(Checksum.Encapsulate(redone).ToArray());
		}

		private void OnDeviceOutput(byte[] data)
		{
			this.dataLink.Input(data);
		}

		private void DeviceInput(params byte[] data)
		{
			this.device.Input(data);
		}
	}
}

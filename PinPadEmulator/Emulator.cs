using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Commands.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinPadEmulator
{
	public class Emulator
	{
		private static readonly byte[] ABORT_ACKNOWLEDGE = new byte[] { 0x4 };
		private static readonly byte[] ACKNOWLEDGE = new byte[] { 0x6 };
		private static readonly byte[] NEGATIVE_ACKNOWLEDGE = new byte[] { 0x15 };

		public event Action<byte[]> Output;
		public event Action<string> CorruptRequest;
		public event Action<string> UnknownRequest;
		public event Action<BaseRequest> UnhandledRequest;
		public event Action AbortRequested;

		private readonly DataLink dataLink;
		private readonly Deserializer deserializer;

		private int responseCounter = 0;

		private Dictionary<Type, Action<BaseRequest>> typeHandlerDictionary = new Dictionary<Type, Action<BaseRequest>>();

		public Emulator()
		{
			this.dataLink = new DataLink();
			this.deserializer = new Deserializer();

			this.dataLink.CommandReceived += this.OnCommandReceived;
			this.dataLink.CorruptCommandReceived += this.OnCorruptCommandReceived;
			this.dataLink.AbortRequested += this.OnAbortRequested;
		}

		public void Input(byte data)
		{
			this.dataLink.Input(data);
		}

		private void OnCommandReceived(string command)
		{
			this.Output?.Invoke(ACKNOWLEDGE);

			var request = this.deserializer.Deserialize(command);

			if (request == null) { this.UnknownRequest?.Invoke(command); return; }

			this.RouteRequest(request);
		}

		private void OnCorruptCommandReceived(string command)
		{
			this.Output?.Invoke(NEGATIVE_ACKNOWLEDGE);

			this.CorruptRequest?.Invoke(command);
		}

		private void OnAbortRequested()
		{
			this.Output?.Invoke(ABORT_ACKNOWLEDGE);

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

		public void Send(BaseResponse response)
		{
			this.responseCounter++;

			var command = response.ToString();
			this.Output?.Invoke(this.dataLink.Encapsulate(command).ToArray());
		}
	}
}

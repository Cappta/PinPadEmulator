using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Commands.Responses;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Crypto
{
	public class ActiveCryptoHandler : BaseCryptoHandler
	{
		public override string Handle(string command)
		{
			if (command.StartsWith("DWK2632") == false) { return null; }

			var random = new Random();
			var decryptedRsaCryptogram = new DecryptedRsaCryptogram();
			decryptedRsaCryptogram.SequentialNumber.Value = random.Next(0, 999999999);
			decryptedRsaCryptogram.WorkingKey.Value = new byte[16];
			random.NextBytes(decryptedRsaCryptogram.WorkingKey.Value);
			this.WorkingKey = decryptedRsaCryptogram.WorkingKey.Value;

			var dwkRequest = new DefineRsaWorkingKeyRequest();
			dwkRequest.Init(new StringReader(command));
			var rsaEngine = new RsaEngine();
			rsaEngine.Init(true, new RsaKeyParameters(false, new BigInteger(1, dwkRequest.Modulus.Value), new BigInteger(1, dwkRequest.Exponent.Value)));

			var unprocessedBlock = Encoding.ASCII.GetBytes(decryptedRsaCryptogram.ToString());

			var dwkResponse = new DefineRsaWorkingKeyResponse();
			dwkResponse.Cryptogram.Value = rsaEngine.ProcessBlock(unprocessedBlock, 0, unprocessedBlock.Length);
			return dwkResponse.ToString();
		}
	}
}

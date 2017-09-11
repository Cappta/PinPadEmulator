using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Commands.Responses;
using PinPadEmulator.Extensions;
using System;
using System.Text;

namespace PinPadEmulator.Crypto
{
	public class ActiveCryptoHandler : BaseCryptoHandler
	{
		private static readonly Random random = new Random();

		public override BaseResponse Handle(string command)
		{
			if (command.TryConvertTo(out DefineRsaWorkingKeyRequest dwkRequest) == false) { return null; }

			var decryptedRsaCryptogram = new DecryptedRsaCryptogram();
			decryptedRsaCryptogram.SequentialNumber.Value = random.Next(0, 999999999);

			this.WorkingKey = random.ByteArray(16);
			decryptedRsaCryptogram.WorkingKey.Value = this.WorkingKey;
			 
			var rsaEngine = new RsaEngine();
			rsaEngine.Init(true,
				new RsaKeyParameters(false, new BigInteger(1, dwkRequest.Modulus.Value), new BigInteger(1, dwkRequest.Exponent.Value))
			);

			var unprocessedBlock = Encoding.ASCII.GetBytes(decryptedRsaCryptogram.ToString());

			return new DefineRsaWorkingKeyResponse(rsaEngine.ProcessBlock(unprocessedBlock, 0, unprocessedBlock.Length));
		}
	}
}

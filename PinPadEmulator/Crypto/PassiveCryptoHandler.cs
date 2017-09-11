using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Commands.Responses;
using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System.Security.Cryptography;
using System.Text;

namespace PinPadEmulator.Crypto
{
	public class PassiveCryptoHandler : BaseCryptoHandler
	{
		private const int RSA_KEY_LENGTH = 1024;

		private RSAParameters requestRSAParameters;
		private RSAParameters responseRSAParameters;

		public override string Undo(string command)
		{
			if (command.TryConvertTo(out DefineRsaWorkingKeyRequest dwkRequest))
			{
				this.requestRSAParameters = new RSAParameters()
				{
					Modulus = dwkRequest.Modulus.Value,
					Exponent = dwkRequest.Exponent.Value
				};

				var rsaProvider = new RSACryptoServiceProvider(RSA_KEY_LENGTH);
				this.responseRSAParameters = rsaProvider.ExportParameters(true);
				dwkRequest.Modulus.Value = this.responseRSAParameters.Modulus;
				dwkRequest.Exponent.Value = this.responseRSAParameters.Exponent;
				return dwkRequest.ToString();
			}
			else if (command.TryConvertTo(out DefineRsaWorkingKeyResponse dwkResponse))
			{
				var privateRsaParameters = new RsaPrivateCrtKeyParameters(
					new BigInteger(1, this.responseRSAParameters.Modulus), new BigInteger(1, this.responseRSAParameters.Exponent),
					new BigInteger(1, this.responseRSAParameters.D), new BigInteger(1, this.responseRSAParameters.P),
					new BigInteger(1, this.responseRSAParameters.Q), new BigInteger(1, this.responseRSAParameters.DP),
					new BigInteger(1, this.responseRSAParameters.DQ), new BigInteger(1, this.responseRSAParameters.InverseQ)
				);
				var rsaEngine = new RsaEngine();
				rsaEngine.Init(false, privateRsaParameters);

				var processedBlock = rsaEngine.ProcessBlock(dwkResponse.Cryptogram.Value, 0, dwkResponse.Cryptogram.Value.Length);
				var decryptedRsaCryptogram = new DecryptedRsaCryptogram();
				decryptedRsaCryptogram.Init(new StringReader(Encoding.ASCII.GetString(processedBlock)));
				this.WorkingKey = decryptedRsaCryptogram.WorkingKey.Value;

				var publicRsaParameters = new RsaKeyParameters(false, new BigInteger(1, this.requestRSAParameters.Modulus),
					new BigInteger(1, this.requestRSAParameters.Exponent));
				rsaEngine.Init(true, publicRsaParameters);
				var unprocessedBlock = Encoding.ASCII.GetBytes(decryptedRsaCryptogram.ToString());
				dwkResponse.Cryptogram.Value = rsaEngine.ProcessBlock(unprocessedBlock, 0, unprocessedBlock.Length);
				return dwkResponse.ToString();
			}
			return base.Undo(command);
		}

		public override BaseResponse Handle(string command)
		{
			return null;
		}
	}
}

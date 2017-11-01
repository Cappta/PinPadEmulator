using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using PinPadEmulator.Extensions;
using PinPadSDK;
using PinPadSDK.Commands.Fields;
using PinPadSDK.Commands.Requests;
using PinPadSDK.Commands.Responses;
using PinPadSDK.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace PinPadEmulator.Crypto
{
	public class PassiveCryptoHandler : BaseCryptoHandler
	{
		private const int RSA_KEY_LENGTH = 1024;

		private RSAParameters requestedRSAParameters;
		private RSAParameters detouredRSAParameters;

		public override string Undo(string command)
		{
			if (command.TryConvertTo(out DefineRsaWorkingKeyRequest dwkRequest))
			{
				this.requestedRSAParameters = new RSAParameters()
				{
					Modulus = dwkRequest.Modulus.Value,
					Exponent = dwkRequest.Exponent.Value
				};

				var rsaProvider = new RSACryptoServiceProvider(RSA_KEY_LENGTH);
				this.detouredRSAParameters = rsaProvider.ExportParameters(true);
				dwkRequest.Modulus.Value = this.detouredRSAParameters.Modulus;
				dwkRequest.Exponent.Value = this.detouredRSAParameters.Exponent;
				return dwkRequest.ToString();
			}
			else if (command.TryConvertTo(out DefineRsaWorkingKeyResponse dwkResponse))
			{
				var privateRsaParameters = new RsaPrivateCrtKeyParameters(
					new BigInteger(1, this.detouredRSAParameters.Modulus), new BigInteger(1, this.detouredRSAParameters.Exponent),
					new BigInteger(1, this.detouredRSAParameters.D), new BigInteger(1, this.detouredRSAParameters.P),
					new BigInteger(1, this.detouredRSAParameters.Q), new BigInteger(1, this.detouredRSAParameters.DP),
					new BigInteger(1, this.detouredRSAParameters.DQ), new BigInteger(1, this.detouredRSAParameters.InverseQ)
				);
				var rsaEngine = new RsaEngine();
				rsaEngine.Init(false, privateRsaParameters);

				var processedBlock = rsaEngine.ProcessBlock(dwkResponse.Cryptogram.Value, 0, dwkResponse.Cryptogram.Value.Length);

				var decryptedRsaCryptogram = new DecryptedRsaCryptogram();
				decryptedRsaCryptogram.Init(new StringReader(Encoding.ASCII.GetString(processedBlock)));

				this.WorkingKey = decryptedRsaCryptogram.WorkingKey.Value;

				var publicRsaParameters = new RsaKeyParameters(false,
					new BigInteger(1, this.requestedRSAParameters.Modulus), new BigInteger(1, this.requestedRSAParameters.Exponent)
				);

				rsaEngine.Init(true, publicRsaParameters);
				var unprocessedBlock = Encoding.ASCII.GetBytes(decryptedRsaCryptogram.ToString());
				dwkResponse.Cryptogram.Value = rsaEngine.ProcessBlock(unprocessedBlock, 0, unprocessedBlock.Length);

				return dwkResponse.ToString();
			}
			return base.Undo(command);
		}
	}
}

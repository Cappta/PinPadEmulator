using PinPadEmulator.Extensions;
using PinPadSDK.Commands.Requests;
using PinPadSDK.Commands.Responses;
using PinPadSDK.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PinPadEmulator.Crypto
{
	public abstract class BaseCryptoHandler : ICryptoHandler
	{
		private static readonly Regex TRACK1_PAN_REGEX = new Regex(@"B([\d ABCDEFabcdef]+)\^");
		private static readonly Regex TRACK2_PAN_REGEX = new Regex(@"([\d ABCDEFabcdef]+)=");

		private const int ENCRYPTED_PAN_BLOCK_LENGTH = 16;

		public event Action<byte[]> WorkingKeyDefined;

		private byte[] workingKey;
		public byte[] WorkingKey
		{
			get { return this.workingKey; }
			set
			{
				this.workingKey = value;
				this.encryptedPanToPanDictionary.Clear();
				this.panToEncryptedPanDictionary.Clear();
				this.WorkingKeyDefined?.Invoke(value);
			}
		}

		private Dictionary<string, string> encryptedPanToPanDictionary = new Dictionary<string, string>();
		private Dictionary<string, string> panToEncryptedPanDictionary = new Dictionary<string, string>();

		public virtual string Undo(string command)
		{
			if (command.TryConvertTo(out GetCardResponse getCardResponse))
			{
				var decryptedPan = default(string);

				var track1Pan = getCardResponse.Track1.Value?.Pan.Value;
				if (track1Pan != null)
				{
					decryptedPan = this.DecryptPan(track1Pan);
					getCardResponse.Track1.Value.Pan.Value = decryptedPan;
				}

				var track2Pan = getCardResponse.Track2.Value?.Pan.Value;
				if (track2Pan != null)
				{
					if (decryptedPan == null) { decryptedPan = this.DecryptPan(track2Pan); }
					getCardResponse.Track2.Value.Pan.Value = decryptedPan;
				}

				if (getCardResponse.Pan.Value != null)
				{
					if (decryptedPan == null) { decryptedPan = this.DecryptPan(getCardResponse.Pan.Value); }
					getCardResponse.Pan.Value = decryptedPan;
				}

				return getCardResponse.ToString();
			}
			else if (command.TryConvertTo(out GetPinRequest getPinRequest))
			{
				getPinRequest.Pan.Value = this.EncryptPan(getPinRequest.Pan.Value);

				return getPinRequest.ToString();
			}
			return command;
		}

		public virtual string Redo(string command)
		{
			if (command.TryConvertTo(out GetCardResponse getCardResponse))
			{
				var track1Pan = getCardResponse.Track1.Value?.Pan.Value;
				if (track1Pan != null)
				{
					getCardResponse.Track1.Value.Pan.Value = this.EncryptPan(track1Pan);
				}

				var track2Pan = getCardResponse.Track2.Value?.Pan.Value;
				if (track2Pan != null)
				{
					getCardResponse.Track2.Value.Pan.Value = this.EncryptPan(track2Pan);
				}

				if (getCardResponse.Pan.Value != null)
				{
					getCardResponse.Pan.Value = this.EncryptPan(getCardResponse.Pan.Value);
				}

				return getCardResponse.ToString();
			}
			else if (command.TryConvertTo(out GetPinRequest getPinRequest))
			{
				getPinRequest.Pan.Value = this.DecryptPan(getPinRequest.Pan.Value);

				return getPinRequest.ToString();
			}
			return command;
		}

		public virtual BaseResponse Handle(string command)
		{
			return null;
		}

		private string DecryptPan(string encryptedPan)
		{
			if (string.IsNullOrWhiteSpace(encryptedPan) || this.WorkingKey == null) { return encryptedPan; }
			if (this.encryptedPanToPanDictionary.ContainsKey(encryptedPan)) { return this.encryptedPanToPanDictionary[encryptedPan]; }

			var tripleDesEngine = new TripleDESCryptoServiceProvider() { Key = this.workingKey, Mode = CipherMode.ECB, Padding = PaddingMode.None };
			var decryptor = tripleDesEngine.CreateDecryptor();

			var exceedingDigitCount = encryptedPan.Length - ENCRYPTED_PAN_BLOCK_LENGTH;
			var firstDigits = new string(encryptedPan.Take(exceedingDigitCount).ToArray());
			var lastDigits = new string(encryptedPan.Skip(exceedingDigitCount).ToArray());

			var encryptedPanData = lastDigits.GetBytesFromHexString();
			var decryptedPanData = decryptor.TransformFinalBlock(encryptedPanData, 0, encryptedPanData.Length);
			var decryptedlastDigits = decryptedPanData.ToHexString();
			var restoredWhiteSpaces = Regex.Replace(decryptedlastDigits, @"[eE]", " ");
			var cleanLastDigits = Regex.Replace(restoredWhiteSpaces, @"[fF]", "");

			var pan = firstDigits + cleanLastDigits;

			this.CacheEncryptedPan(pan, encryptedPan);

			return pan;
		}

		private string EncryptPan(string pan)
		{
			if (string.IsNullOrWhiteSpace(pan) || this.WorkingKey == null) { return pan; }
			if (this.panToEncryptedPanDictionary.ContainsKey(pan)) { return this.panToEncryptedPanDictionary[pan]; }

			var tripleDesEngine = new TripleDESCryptoServiceProvider() { Key = this.workingKey, Mode = CipherMode.ECB, Padding = PaddingMode.None };
			var encryptor = tripleDesEngine.CreateEncryptor();

			var exceedingDigitCount = pan.Length - ENCRYPTED_PAN_BLOCK_LENGTH;
			var firstDigits = new string(pan.Take(exceedingDigitCount).ToArray());
			var lastDigits = new string(pan.Skip(exceedingDigitCount).ToArray());

			var replacedWhiteSpace = lastDigits.Replace(" ", "E");
			var paddedPan = replacedWhiteSpace.PadRight(ENCRYPTED_PAN_BLOCK_LENGTH, 'F');
			var decryptedPanData = paddedPan.GetBytesFromHexString();
			var encryptedPanData = encryptor.TransformFinalBlock(decryptedPanData, 0, decryptedPanData.Length);
			var encryptedlastDigits = encryptedPanData.ToHexString();

			var encryptedPan = firstDigits + encryptedlastDigits;

			this.CacheEncryptedPan(pan, encryptedPan);

			return encryptedPan;
		}

		private void CacheEncryptedPan(string pan, string encryptedPan)
		{
			this.encryptedPanToPanDictionary.Add(encryptedPan, pan);
			this.panToEncryptedPanDictionary.Add(pan, encryptedPan);
		}

		private string ExtractPanWithRegex(string input, Regex regex)
		{
			if (string.IsNullOrWhiteSpace(input) || regex.IsMatch(input) == false) { return null; }

			var match = regex.Match(input);

			return match.Groups[1].Value;
		}
	}
}

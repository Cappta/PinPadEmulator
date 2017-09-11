using PinPadEmulator.Commands;
using PinPadEmulator.Commands.Requests;
using PinPadEmulator.Commands.Responses;
using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
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
				this.encryptedPanDictionary.Clear();
				this.decryptedPanDictionary.Clear();
				this.WorkingKeyDefined?.Invoke(value);
			}
		}

		private Dictionary<string, string> encryptedPanDictionary = new Dictionary<string, string>();
		private Dictionary<string, string> decryptedPanDictionary = new Dictionary<string, string>();

		public virtual string Undo(string command)
		{
			if (command.TryConvertTo(out GetCardResponse getCardResponse))
			{
				var decryptedPan = default(string);

				var track1Pan = this.ExtractPanWithRegex(getCardResponse.Track1.Value, TRACK1_PAN_REGEX);
				if (track1Pan != null)
				{
					decryptedPan = this.DecryptPan(track1Pan);
					getCardResponse.Track1.Value = TRACK1_PAN_REGEX.Replace(getCardResponse.Track1.Value, $"B{decryptedPan}^");
				}

				var track2Pan = this.ExtractPanWithRegex(getCardResponse.Track2.Value, TRACK2_PAN_REGEX);
				if (track2Pan != null)
				{
					if (decryptedPan == null) { decryptedPan = this.DecryptPan(track2Pan); }
					getCardResponse.Track2.Value = TRACK2_PAN_REGEX.Replace(getCardResponse.Track2.Value, $"{decryptedPan}=");
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
				var track1Pan = this.ExtractPanWithRegex(getCardResponse.Track1.Value, TRACK1_PAN_REGEX);
				if (track1Pan != null)
				{
					getCardResponse.Track1.Value = TRACK1_PAN_REGEX.Replace(getCardResponse.Track1.Value, $"B{this.EncryptPan(track1Pan)}^");
				}

				var track2Pan = this.ExtractPanWithRegex(getCardResponse.Track2.Value, TRACK2_PAN_REGEX);
				if (track2Pan != null)
				{
					getCardResponse.Track2.Value = TRACK2_PAN_REGEX.Replace(getCardResponse.Track2.Value, $"{this.EncryptPan(track2Pan)}=");
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

		public abstract string Handle(string command);

		private string DecryptPan(string encryptedPan)
		{
			if(string.IsNullOrWhiteSpace(encryptedPan) || this.WorkingKey == null) { return encryptedPan; }
			if (this.encryptedPanDictionary.ContainsKey(encryptedPan)) { return this.encryptedPanDictionary[encryptedPan]; }

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

			var decryptedPan = firstDigits + cleanLastDigits;

			this.encryptedPanDictionary.Add(encryptedPan, decryptedPan);
			this.decryptedPanDictionary.Add(decryptedPan, encryptedPan);

			return decryptedPan;
		}

		private string EncryptPan(string pan)
		{
			if (string.IsNullOrWhiteSpace(pan) || this.WorkingKey == null) { return pan; }
			if (this.decryptedPanDictionary.ContainsKey(pan)) { return this.decryptedPanDictionary[pan]; }

			var tripleDesEngine = new TripleDESCryptoServiceProvider() { Key = this.workingKey, Mode = CipherMode.ECB, Padding = PaddingMode.None };
			var encryptor = tripleDesEngine.CreateEncryptor();

			var exceedingDigitCount = pan.Length - ENCRYPTED_PAN_BLOCK_LENGTH;
			var firstDigits = new string(pan.Take(exceedingDigitCount).ToArray());
			var lastDigits = new string(pan.Skip(exceedingDigitCount).ToArray());

			var replacedWhiteSpace = lastDigits.Replace(" ", "E");
			var paddedPan = replacedWhiteSpace.PadRight(ENCRYPTED_PAN_BLOCK_LENGTH, 'F');
			var decryptedPanData= paddedPan.GetBytesFromHexString();
			var encryptedPanData = encryptor.TransformFinalBlock(decryptedPanData, 0, decryptedPanData.Length);
			var encryptedlastDigits = encryptedPanData.ToHexString();

			var encryptedPan = firstDigits + encryptedlastDigits;

			this.encryptedPanDictionary.Add(encryptedPan, pan);
			this.decryptedPanDictionary.Add(pan, encryptedPan);
			
			return encryptedPan;
		}

		private string ExtractPanWithRegex(string input, Regex regex)
		{
			if (regex.IsMatch(input) == false) { return null; }

			var match = regex.Match(input);

			return match.Groups[1].Value;
		}
	}
}

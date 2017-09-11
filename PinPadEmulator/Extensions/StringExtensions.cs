using PinPadEmulator.Commands;
using PinPadEmulator.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PinPadEmulator.Extensions
{
	internal static class StringExtensions
	{
		public static T ConvertTo<T>(this string value)
		{
			var typeOfT = typeof(T);
			if (String.IsNullOrWhiteSpace(value)) { return default(T); }

			var underlyingType = default(Type);
			if ((underlyingType = Nullable.GetUnderlyingType(typeOfT)) != null)
			{
				typeOfT = underlyingType;
			}

			if (typeOfT.IsEnum)
			{
				underlyingType = Enum.GetUnderlyingType(typeOfT);
				var underlyingValue = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
				return (T)underlyingValue;
			}

			if (typeOfT == typeof(bool) && int.TryParse(value, out var intValue))
			{
				return (T)(object)(intValue == 1);
			}

			if (typeOfT == typeof(byte[]))
			{
				return (T)(object)value.GetBytesFromHexString();
			}

			if(typeof(BaseCommand).IsAssignableFrom(typeOfT))
			{
				var instantiatedCommand = (BaseCommand)Activator.CreateInstance(typeOfT);
				instantiatedCommand.Init(new StringReader(value));
				return (T)(object)instantiatedCommand;
			}

			return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
		}

		public static bool TryConvertTo<T>(this string value, out T output)
		{
			try
			{
				output = value.ConvertTo<T>();
				return true;
			}
			catch
			{
				output = default(T);
				return false;
			}
		}

		public static DateTime ConvertToDateTime(this string value, string format)
		{
			return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
		}

		public static byte[] GetBytesFromHexString(this string value)
		{
			return value.Split(2).Select(d => Convert.ToByte(d, 16)).ToArray();
		}

		public static IEnumerable<string> Split(this string value, int length)
		{
			var stringReader = new StringReader(value);
			while (stringReader.Remaining > 0)
			{
				yield return stringReader.Read(Math.Min(length, stringReader.Remaining));
			}
		}

		public static bool EqualsIgnoreCase(this string first, string second)
		{
			return first.Equals(second, StringComparison.OrdinalIgnoreCase);
		}
	}
}

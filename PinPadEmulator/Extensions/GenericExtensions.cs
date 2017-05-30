using System;

namespace PinPadEmulator.Extensions
{
	internal static class GenericExtensions
	{
		public static string Serialize<type>(this type value)
		{
			var typeOfType = typeof(type);

			var underlyingType = default(Type);
			if ((underlyingType = Nullable.GetUnderlyingType(typeOfType)) != null)
			{
				typeOfType = underlyingType;
			}

			if (typeOfType.IsEnum)
			{
				var baseType = Enum.GetUnderlyingType(typeOfType);
				var baseValue = Convert.ChangeType(value, baseType);
				return baseValue.ToString();
			}

			if (typeOfType == typeof(bool))
			{
				var boolValue = (bool)(object)value;
				return boolValue ? "1" : "0";
			}

			if (value == null) { return string.Empty; }

			return value.ToString();
		}
	}
}

using ProductsAPI.Domain.Annotations;
using System.Reflection;

namespace ProductsAPI.Domain.Extensions;

public static class EnumExtensions
{
	public static string GetLocalizedValue(this Enum enumValue)
	{
		FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

		LocalizedStringAttribute[] attributes =
			(LocalizedStringAttribute[])fi.GetCustomAttributes(
			typeof(LocalizedStringAttribute),
			false);

		if (attributes != null &&
			attributes.Length > 0)
			return attributes[0].ToLocalizedString();
		else
			return enumValue.ToString();
	}
}

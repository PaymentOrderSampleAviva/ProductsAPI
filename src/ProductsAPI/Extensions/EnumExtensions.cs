using ProductsAPI.Annotations;
using ProductsAPI.PaymentProcessors;
using System.Reflection;

namespace ProductsAPI.Extensions;

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

	public static Type? GetPaymentProcessorType(this Enum enumValue)
	{
		FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

		PaymentProcessorTypeAttribute[] attributes =
			(PaymentProcessorTypeAttribute[])fi.GetCustomAttributes(
			typeof(PaymentProcessorTypeAttribute),
			false);

		if (attributes != null &&
			attributes.Length > 0)
			return attributes[0].ProcessorType;
		
		throw new NotImplementedException();
	}
}

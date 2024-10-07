using ProductsAPI.PaymentsGateway.Annotations;
using System.Reflection;

namespace ProductsAPI.PaymentsGateway.Extensions;

public static class EnumExtensions
{
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

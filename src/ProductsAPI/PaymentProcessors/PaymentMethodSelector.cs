using ProductsAPI.Extensions;
using ProductsAPI.PaymentProcessors.Abstractions;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class PaymentMethodSelector(IServiceProvider serviceProvider) : IPaymentMethodSelector
{
    public IPaymentProcessor? Select(PaymentMethod method)
    {
        var type = method.GetPaymentProcessorType();
        type.ThrowIfNull();

        var processor = serviceProvider.GetService(type) as IPaymentProcessor;
        return processor;
    }
}

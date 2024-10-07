using ProductsAPI.PaymentsGateway.Abstractions;
using ProductsAPI.PaymentsGateway.Extensions;
using Throw;

namespace ProductsAPI.PaymentsGateway;

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

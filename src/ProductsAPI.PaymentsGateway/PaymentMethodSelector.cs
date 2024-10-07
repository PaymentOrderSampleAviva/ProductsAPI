using ProductsAPI.PaymentsGateway.Abstractions;
using ProductsAPI.PaymentsGateway.Extensions;
using Throw;

namespace ProductsAPI.PaymentsGateway;

public class PaymentMethodSelector(IServiceProvider serviceProvider) : IPaymentMethodSelector
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IPaymentProcessor? Select(PaymentMethod method)
    {
        var type = method.GetPaymentProcessorType();
        type.ThrowIfNull();

        var processor = _serviceProvider.GetService(type) as IPaymentProcessor;
        return processor;
    }
}

using ProductsAPI.Extensions;
using ProductsAPI.PaymentProcessors.Abstractions;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class PaymentMethodSelector(IServiceProvider serviceProvider) : IPaymentMethodSelector
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IPaymentProcessor? Select(PaymentMethod method)
    {
        var type = method.GetPaymentProcessorType();
        type.ThrowIfNull();

        return _serviceProvider.GetService(type) as IPaymentProcessor;
    }
}

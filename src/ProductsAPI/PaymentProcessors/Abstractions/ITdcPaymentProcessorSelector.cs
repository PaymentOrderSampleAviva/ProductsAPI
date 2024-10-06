namespace ProductsAPI.PaymentProcessors.Abstractions;

public interface ITdcPaymentProcessorSelector
{
    IPaymentProcessor? Select(double totalAmount);
}

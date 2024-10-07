namespace ProductsAPI.PaymentProcessors.Abstractions;

public interface IPaymentProcessorSelector
{
    IPaymentProcessor? Select(double totalAmount);
}

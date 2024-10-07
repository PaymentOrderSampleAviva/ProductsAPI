namespace ProductsAPI.PaymentsGateway.Abstractions;

public interface IPaymentProcessorSelector
{
    IPaymentProcessor? Select(double totalAmount);
}

namespace ProductsAPI.PaymentProcessors.Abstractions;

public interface IPaymentMethodSelector
{
	IPaymentProcessor? Select(PaymentMethod method);
}

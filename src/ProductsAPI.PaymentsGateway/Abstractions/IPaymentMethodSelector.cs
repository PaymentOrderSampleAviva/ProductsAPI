namespace ProductsAPI.PaymentsGateway.Abstractions;

public interface IPaymentMethodSelector
{
	IPaymentProcessor? Select(PaymentMethod method);
}

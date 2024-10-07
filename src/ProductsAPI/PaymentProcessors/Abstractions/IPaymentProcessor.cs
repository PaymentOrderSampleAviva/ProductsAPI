using ProductsAPI.PaymentProcessors.Model;

namespace ProductsAPI.PaymentProcessors.Abstractions
{
    public interface IPaymentProcessor
    {
        Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default);
    }
}

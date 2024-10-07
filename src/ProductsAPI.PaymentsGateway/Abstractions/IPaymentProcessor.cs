using ProductsAPI.PaymentsGateway.Model;

namespace ProductsAPI.PaymentsGateway.Abstractions
{
    public interface IPaymentProcessor
    {
        Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default);
    }
}

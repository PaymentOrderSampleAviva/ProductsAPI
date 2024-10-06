using ProductsAPI.PaymentProcessors;

namespace ProductsAPI.DataContracts
{
	public class CreateOrderRequest
	{
        public required PaymentMethod Method { get; set; }
        public required List<OrderProductRequest> Products { get; set; }

        public double GetTotalAmount() => Products.Sum(p => p.UnitPrice);
    }
}

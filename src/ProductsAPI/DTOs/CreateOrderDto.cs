using ProductsAPI.PaymentProcessors;

namespace ProductsAPI.DTOs
{
	public class CreateOrderDto
	{
        public required PaymentMethod Method { get; set; }
        public required List<OrderProductDto> Products { get; set; }

        public double GetTotalAmount() => Products.Sum(p => p.UnitPrice);
    }
}

using ProductsAPI.PaymentsGateway;

namespace ProductsAPI.Application.DTOs
{
	public class CreateOrderDto
	{
        public required PaymentMethod Method { get; set; }
        public required List<OrderItemDto> Products { get; set; }

        public double GetTotalAmount() => Products.Sum(p => p.UnitPrice);
    }
}

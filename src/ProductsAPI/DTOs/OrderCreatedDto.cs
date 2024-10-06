namespace ProductsAPI.DTOs
{
	public class OrderCreatedDto
	{
        public required List<OrderProductDto> Products { get; set; }
    }
}

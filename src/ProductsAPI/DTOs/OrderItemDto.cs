namespace ProductsAPI.DTOs
{
	public class OrderItemDto
	{
		public int ProductId { get; set; }
		public required string Name { get; set; }
		public string? Details { get; set; }
		public double UnitPrice { get; set; }
	}
}

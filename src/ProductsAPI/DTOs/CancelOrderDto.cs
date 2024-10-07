namespace ProductsAPI.DTOs
{
	public class CancelOrderDto
	{
        public int OrderId { get; set; }
        public string? Reason { get; set; }
    }
}

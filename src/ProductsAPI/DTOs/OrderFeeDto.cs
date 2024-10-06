namespace ProductsAPI.DTOs
{
	public class OrderFeeDto
	{
        public int Id { get; set; }
		public required string Name { get; set; }
		public double Amount { get; set; }
	}
}

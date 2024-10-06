namespace ProductsAPI.Models;

#nullable disable
public class OrderFee : EntityBase
{
	public required string Name { get; set; }
	public double Amount { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
}

namespace ProductsAPI.Models;

public class Product : EntityBase
{
	public required string Name { get; set; }
	public string? Details { get; set; }
	public string Status { get; set; }
	public double UnitPrice { get; set; }
}

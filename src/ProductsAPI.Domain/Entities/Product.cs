namespace ProductsAPI.Domain.Entities;

public class Product : EntityBase
{
	public required string Name { get; set; }
	public string? Details { get; set; }
	public ProductStatus Status { get; set; } = ProductStatus.NotAvailable;
	public double UnitPrice { get; set; } = 0;
    public ICollection<OrderItem> OrderItems { get; set; }
}

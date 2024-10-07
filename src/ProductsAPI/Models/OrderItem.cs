namespace ProductsAPI.Models;

#nullable disable
public class OrderItem : EntityBase
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
	public Product Product { get; set; }
    public double UnitPrice { get; set; }
}

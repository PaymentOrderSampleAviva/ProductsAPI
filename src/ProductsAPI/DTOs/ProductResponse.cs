using ProductsAPI.Models;

namespace ProductsAPI.DTOs;

public class ProductResponse
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public string? Details { get; set; }
    public int StatusId { get; set; }
    public required string Status { get; set; }
	public double UnitPrice { get; set; } = 0;
}

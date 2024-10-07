using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.DTOs;

public class ProductDto
{
	public int ProductId { get; set; }
	public required string Name { get; set; }
	public string? Details { get; set; }
    public int StatusId { get; set; }
    public required string Status { get; set; }
	public double UnitPrice { get; set; } = 0;
}

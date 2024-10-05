using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Models;

namespace ProductsAPI.Data.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
		builder.Property(e => e.Details).HasMaxLength(250);

		// Data seed
		builder.HasData(GetDataSeed());
	}

	public IReadOnlyList<Product> GetDataSeed()
	{
		return new List<Product>
		{
			new Product{ Id = 1, Name = "DELL Inspiron 5050", Details = "32GB / 512 SSD / i7 2.2Ghz ", Status = ProductStatus.Available, UnitPrice = 2350.00 },
			new Product{ Id = 2, Name = "Lenovo 3450", Details = "16GB / 512 SSD / i5 1.6Ghz ", Status = ProductStatus.Available, UnitPrice = 2509.00 },
			new Product{ Id = 3, Name = "Desk Flexible", Details = "4 Positions ", Status = ProductStatus.Available, UnitPrice = 3580.00 },
			new Product{ Id = 4, Name = "Headset 450p", Details = "Logitech Active Noise Can", Status = ProductStatus.NotAvailable, UnitPrice = 459.00 },
		};
	}
}

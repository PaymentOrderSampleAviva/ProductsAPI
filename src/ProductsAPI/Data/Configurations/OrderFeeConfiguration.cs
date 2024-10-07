using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Models;

namespace ProductsAPI.Data.Configurations
{
	public class OrderFeeConfiguration : IEntityTypeConfiguration<OrderFee>
	{
		public void Configure(EntityTypeBuilder<OrderFee> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(50);
		}
	}
}

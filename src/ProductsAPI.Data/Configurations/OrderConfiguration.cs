using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(e => e.Id);
			builder.Property(p => p.CancelReason).HasMaxLength(50);
			builder.Property(p => p.RevertReason).HasMaxLength(50);
			builder.Property(p => p.PaymentMethod).HasMaxLength(20);
			builder.Property(p => p.PaymentOrderId).HasMaxLength(50);

			builder.HasMany(e => e.Items).WithOne(e => e.Order);
			builder.HasMany(e => e.Fees).WithOne(e => e.Order);
		}
	}
}

using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Application.DTOs
{
	public class OrderDto
	{
		public int Id { get; set; }
		public List<OrderItemDto> Items { get; set; }
		public List<OrderFeeDto> Fees { get; set; }
		public required string Status { get; set; }
		public string? CancelReason { get; set; }
		public string? RevertReason { get; set; }
		public required string PaymentMethod { get; set; }
		public string? PaymentOrderId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }

	}
}

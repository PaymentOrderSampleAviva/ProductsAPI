using Throw;

namespace ProductsAPI.Models;

#nullable disable
public class Order
{
	public int Id { get; set; }
	public ICollection<OrderItem> Items { get; set; }
    public ICollection<OrderFee> Fees { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Processing;
    public string CancelReason { get; set; }
    public string RevertReason { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentOrderId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

	public void Cancel(string reason)
	{
		Status.Throw().IfGreaterThan(OrderStatus.Confirmed);
		CancelReason = reason;
		UpdatedDate = DateTime.UtcNow;
	}

	public void Confirm(string paymentOrderId, IEnumerable<OrderFee> orderFees)
	{
		Status.Throw().IfNotEquals(OrderStatus.Processing);

		PaymentOrderId = paymentOrderId;
		
		if (Fees == null)
		{
			Fees = new List<OrderFee>();
		}

		foreach (var fee in orderFees)
		{
			Fees.Add(fee);
		}

		Status = OrderStatus.Confirmed;
		UpdatedDate = DateTime.UtcNow;
	}
}

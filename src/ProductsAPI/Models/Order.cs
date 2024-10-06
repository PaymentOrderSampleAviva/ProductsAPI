namespace ProductsAPI.Models;

#nullable disable
public class Order
{
	public int Id { get; set; }
	public ICollection<OrderItem> Items { get; set; }
    public ICollection<OrderFee> Fees { get; set; }
    public OrderStatus Status { get; set; }
    public string CancelReason { get; set; }
    public string RevertReason { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentOrderId { get; set; }
    public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
}

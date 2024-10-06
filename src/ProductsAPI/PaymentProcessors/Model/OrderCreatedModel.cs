namespace ProductsAPI.PaymentProcessors.Model;

public class OrderCreatedModel
{
    public required Guid OrderId { get; set; }
	public required List<ProductModel> Products { get; set; }
    public required List<FeeModel> Fees { get; set; }
}

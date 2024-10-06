namespace ProductsAPI.PaymentProcessors.Model;

public class CreateOrderModel
{
    public required List<ProductModel> Products { get; set; }
}

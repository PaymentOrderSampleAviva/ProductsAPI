namespace ProductsAPI.PaymentProcessors.Model;

public class CreateOrderModel
{
    public required string Method { get; set; }
    public required List<ProductModel> Products { get; set; }

    public double GetTotalAmount() => Products?.Sum(x => x.UnitPrice) ?? 0;
}

namespace ProductsAPI.PaymentProcessors.Model;

public class CreateOrderModel
{
    public required List<ProductModel> Products { get; set; }

    public double GetTotalAmount() => Products?.Sum(x => x.UnitPrice) ?? 0;
}

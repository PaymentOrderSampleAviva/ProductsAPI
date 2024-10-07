using System.Text.Json.Serialization;

namespace ProductsAPI.PaymentsGateway.Model;

public class CreateOrderModel
{
    [JsonPropertyName("method")]
    public required string Method { get; set; }
	[JsonPropertyName("products")]
	public required List<ProductModel> Products { get; set; }

    public double GetTotalAmount() => Products?.Sum(x => x.UnitPrice) ?? 0;
}

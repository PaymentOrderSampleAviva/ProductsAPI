using System.Text.Json.Serialization;

namespace ProductsAPI.PaymentsGateway.Model;

public class OrderCreatedModel
{
    [JsonPropertyName("orderId")]
    public required string OrderId { get; set; }
	[JsonPropertyName("products")]
	public required List<ProductModel> Products { get; set; }
	[JsonPropertyName("fees")]
	public required List<FeeModel> Fees { get; set; }
}

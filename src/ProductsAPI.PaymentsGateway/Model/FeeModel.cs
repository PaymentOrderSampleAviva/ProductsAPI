using System.Text.Json.Serialization;

namespace ProductsAPI.PaymentsGateway.Model;

public class FeeModel
{
	[JsonPropertyName("name")]
	public required string Name { get; set; }
	[JsonPropertyName("amount")]
	public double Amount { get; set; }
}

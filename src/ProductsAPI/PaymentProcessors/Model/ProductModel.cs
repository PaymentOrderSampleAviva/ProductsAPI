using System.Text.Json.Serialization;

namespace ProductsAPI.PaymentProcessors.Model
{
	public class ProductModel
	{
		[JsonPropertyName("name")]
		public required string Name { get; set; }
		[JsonPropertyName("unitPrice")]
		public double UnitPrice { get; set; }
	}
}

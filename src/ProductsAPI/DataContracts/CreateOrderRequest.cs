namespace ProductsAPI.DataContracts
{
	public class CreateOrderRequest
	{
        public required List<OrderProductRequest> Products { get; set; }
    }
}

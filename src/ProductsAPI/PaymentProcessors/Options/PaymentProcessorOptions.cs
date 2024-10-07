namespace ProductsAPI.PaymentProcessors.Options
{
	public class PaymentProcessorOptions
	{
		public const string CazaPagos = "CazaPagos";
		public const string PagaFacil = "PagaFacil";

		public required string BaseUrl { get; set; }
        public required string ApiKey { get; set; }
    }
}

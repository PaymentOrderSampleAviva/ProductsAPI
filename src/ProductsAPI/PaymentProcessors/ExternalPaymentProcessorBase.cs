using ProductsAPI.PaymentProcessors.Model;
using System.Text.Json;

namespace ProductsAPI.PaymentProcessors
{
	public class ExternalPaymentProcessorBase(HttpClient httpClient, ILogger logger) : PaymentProcessorBase, IDisposable
	{
		public override async Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel)
		{
			try
			{
				orderModel.Method = TranslatePaymentMethod(orderModel.Method);
				var httpResponse = await httpClient.PostAsJsonAsync("/Order", orderModel);

				if (httpResponse != null && httpResponse.IsSuccessStatusCode)
				{
					//var payload = await httpResponse.Content.ReadAsStringAsync();
					var ordedCreated = await JsonSerializer.DeserializeAsync<OrderCreatedModel>(await httpResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
					return ordedCreated;
				}
				else
				{
					logger.LogError($"Call to external api process order ends with estatus code: {httpResponse?.StatusCode}", [orderModel]);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				throw new PaymentProcessException(ex);
			}

			throw new PaymentProcessException();
		}

		protected virtual string TranslatePaymentMethod(string method) => method;

		public void Dispose() => httpClient?.Dispose();
	}
}

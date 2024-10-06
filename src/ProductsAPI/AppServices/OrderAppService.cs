using AutoMapper;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;
using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;
using Throw;

namespace ProductsAPI.AppServices;

public class OrderAppService (IPaymentMethodSelector paymentMethodSelector, IMapper mapper) : IOrderAppService
{
	private readonly IPaymentMethodSelector _paymentMethodSelector = paymentMethodSelector;
	private readonly IMapper _mapper = mapper;

	public async Task<object> CreateOrderAsync(CreateOrderDto request)
	{
		request.ThrowIfNull();
		request.Products.Throw().IfCountLessThan(1);

		var createOrderModel = _mapper.Map<CreateOrderModel>(request);
		var paymentProcessor = _paymentMethodSelector.Select(request.Method);		
		var response = await paymentProcessor.CreateOrderAsync(createOrderModel);

		return _mapper.Map<OrderCreatedDto>(response);
	}
}

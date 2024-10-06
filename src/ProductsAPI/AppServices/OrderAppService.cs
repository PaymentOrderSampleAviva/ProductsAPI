using AutoMapper;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;
using ProductsAPI.Models;
using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;
using ProductsAPI.Repositories;
using Throw;

namespace ProductsAPI.AppServices;

public class OrderAppService (IOrdersRepository ordersRepository,
	IPaymentMethodSelector paymentMethodSelector,
	IMapper mapper, ILogger<OrderAppService> logger) : IOrderAppService
{
	private readonly IOrdersRepository _ordersRepository = ordersRepository;
	private readonly IPaymentMethodSelector _paymentMethodSelector = paymentMethodSelector;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger _logger = logger;

	public async Task<IReadOnlyList<OrderDto>> ListOrdersAsync()
	{
		var orders = await _ordersRepository.GetAllAsync();
		return _mapper.Map<List<OrderDto>>(orders);
	}

	public async Task<OrderDto> CreateOrderAsync(CreateOrderDto request)
	{
		request.ThrowIfNull();
		request.Products.Throw().IfCountLessThan(1);

		// Save order to db
		var orderEntity = _mapper.Map<Order>(request);
		orderEntity.Id = Random.Shared.Next(1000000); // for in memory db only.
		await _ordersRepository.AddAsync(orderEntity);

		try
		{
			var createOrderModel = _mapper.Map<CreateOrderModel>(request);
			var paymentProcessor = _paymentMethodSelector.Select(request.Method);

			if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

			var orderCreated = await paymentProcessor.CreateOrderAsync(createOrderModel);

			var orderFeeds = _mapper.Map<List<OrderFee>>(orderCreated.Fees);
			orderEntity.Confirm(orderCreated.OrderId.ToString(), orderFeeds);

			return _mapper.Map<OrderDto>(orderEntity);
		}
		catch (Exception ex)
		{
			var orderDto = await CancelOrderAsync(orderEntity.Id, "Exception occurs processing payment.");
			_logger.LogError(ex, "An exception occurs trying to process the payment", [orderDto.Id]);
			throw;
		}
	}

	public async Task<OrderDto> CancelOrderAsync(int orderId, string reason)
	{
		orderId.Throw().IfLessThan(1);

		var orderEntity = await _ordersRepository.GetAsync(orderId, true);
		orderEntity.ThrowIfNull();
		orderEntity.Cancel(reason);
		await _ordersRepository.UpdateAsync(orderEntity);
		return _mapper.Map<OrderDto>(orderEntity);
	}
}

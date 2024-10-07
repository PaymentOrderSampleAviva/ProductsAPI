using AutoMapper;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;
using ProductsAPI.Models;
using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;
using ProductsAPI.Repositories;
using Throw;

namespace ProductsAPI.AppServices;

public class OrdersAppService (IOrdersRepository ordersRepository,
	IPaymentMethodSelector paymentMethodSelector,
	IMapper mapper, ILogger<OrdersAppService> logger) : IOrdersAppService
{
	private readonly IOrdersRepository _ordersRepository = ordersRepository;
	private readonly IPaymentMethodSelector _paymentMethodSelector = paymentMethodSelector;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger _logger = logger;

	public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default)
	{
		var orders = await _ordersRepository.GetAllAsync(cancellationToken);
		return _mapper.Map<List<OrderDto>>(orders);
	}

	public async Task<OrderDto> CreateOrderAsync(CreateOrderDto request, CancellationToken cancellationToken = default)
	{
		request.ThrowIfNull();
		request.Products.Throw().IfCountLessThan(1);

		// Save order to db
		var orderEntity = _mapper.Map<Order>(request);
		await _ordersRepository.AddAsync(orderEntity, cancellationToken);

		try
		{
			var createOrderModel = _mapper.Map<CreateOrderModel>(request);
			var paymentProcessor = _paymentMethodSelector.Select(request.Method);

			if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

			var orderCreated = await paymentProcessor.CreateOrderAsync(createOrderModel, cancellationToken);

			var orderFeeds = _mapper.Map<List<OrderFee>>(orderCreated.Fees);
			orderEntity.Confirm(orderCreated.OrderId, orderFeeds);
			await _ordersRepository.UpdateAsync(orderEntity, cancellationToken);

			return _mapper.Map<OrderDto>(orderEntity);
		}
		catch (Exception ex)
		{
			var orderDto = await CancelOrderAsync(orderEntity.Id, "Exception occurs processing payment."); //cancellation token is not used here because we want to cancel the exinting order.
			_logger.LogError(ex, "An exception occurs trying to process the payment", [orderDto.Id]);
			throw;
		}
	}

	public async Task<OrderDto> CancelOrderAsync(int orderId, string reason, CancellationToken cancellationToken = default)
	{
		orderId.Throw().IfLessThan(1);

		var orderEntity = await _ordersRepository.GetAsync(orderId, true, cancellationToken);
		orderEntity.ThrowIfNull();
		orderEntity.Cancel(reason);
		await _ordersRepository.UpdateAsync(orderEntity, cancellationToken);
		return _mapper.Map<OrderDto>(orderEntity);
	}
}

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
	IProductsRepository productsRepository,
	IPaymentMethodSelector paymentMethodSelector,
	IMapper mapper, ILogger<OrdersAppService> logger) : IOrdersAppService
{
	public async Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default)
	{
		var orders = await ordersRepository.GetAllAsync(cancellationToken);
		return mapper.Map<List<OrderDto>>(orders);
	}

	public async Task<OrderDto> CreateOrderAsync(CreateOrderDto request, CancellationToken cancellationToken = default)
	{
		request.ThrowIfNull();
		request.Products.Throw().IfCountLessThan(1);

		// check product availability
		var productIDs = request.Products.Select(r => r.ProductId).ToList();
		var availableProductsCount = await productsRepository.GetAvailableCountAsync(productIDs, cancellationToken);

		productIDs.Throw(() => new ArgumentException("The product list should contain only available products")).IfCountGreaterThan(availableProductsCount);


		// Save order to db
		var orderEntity = mapper.Map<Order>(request);
		await ordersRepository.AddAsync(orderEntity, cancellationToken);

		try
		{
			var createOrderModel = mapper.Map<CreateOrderModel>(request);
			var paymentProcessor = paymentMethodSelector.Select(request.Method);

			if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

			var orderCreated = await paymentProcessor.CreateOrderAsync(createOrderModel, cancellationToken);

			var orderFeeds = mapper.Map<List<OrderFee>>(orderCreated.Fees);
			orderEntity.Confirm(orderCreated.OrderId, orderFeeds);
			await ordersRepository.UpdateAsync(orderEntity, cancellationToken);

			return mapper.Map<OrderDto>(orderEntity);
		}
		catch (Exception ex)
		{
			var orderDto = await CancelOrderAsync(orderEntity.Id, "Exception occurs processing payment."); //cancellation token is not used here because we want to cancel the exinting order.
			logger.LogError(ex, "An exception occurs trying to process the payment", [orderDto.Id]);
			throw;
		}
	}

	public async Task<OrderDto> CancelOrderAsync(int orderId, string reason, CancellationToken cancellationToken = default)
	{
		orderId.Throw().IfLessThan(1);

		var orderEntity = await ordersRepository.GetAsync(orderId, true, cancellationToken);
		orderEntity.ThrowIfNull();
		orderEntity.Cancel(reason);
		await ordersRepository.UpdateAsync(orderEntity, cancellationToken);
		return mapper.Map<OrderDto>(orderEntity);
	}
}

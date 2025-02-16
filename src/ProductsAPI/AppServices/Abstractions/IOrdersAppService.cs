﻿using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IOrdersAppService
    {
		Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default);

		Task<OrderDto> CreateOrderAsync(CreateOrderDto request, CancellationToken cancellationToken = default);
        Task<OrderDto> CancelOrderAsync(int orderId, string reason, CancellationToken cancellationToken = default);
	}
}

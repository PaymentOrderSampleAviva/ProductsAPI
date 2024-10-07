﻿using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class PaymentProcessor(IPaymentProcessorSelector processorSelector) : PaymentProcessorBase
{
	private readonly IPaymentProcessorSelector _processorSelector = processorSelector;

	public override async Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel)
	{
		orderModel.ThrowIfNull(nameof(orderModel));
		orderModel.Products.Throw().IfCountLessThan(1);

		var paymentProcessor = _processorSelector.Select(orderModel.GetTotalAmount());

		if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

		var response = await paymentProcessor.CreateOrderAsync(orderModel);
		return await Task.FromResult(response);
	}
}
using ProductsAPI.Annotations;

namespace ProductsAPI.PaymentProcessors
{
    public enum PaymentMethod
	{
		[PaymentProcessorType(ProcessorType = typeof(CashPaymentProcessor))]
		[LocalizedString(ResourceKey = "Cash", ResourceType = typeof(PaymentMethodStrings))]
		Cash,
		[PaymentProcessorType(ProcessorType = typeof(TdcPaymentProcessor))]
		[LocalizedString(ResourceKey = "CreditCard", ResourceType = typeof(PaymentMethodStrings))]
		TDC,
		[PaymentProcessorType(ProcessorType = typeof(TransferPaymentProcessor))]
		[LocalizedString(ResourceKey = "Transfer", ResourceType = typeof(PaymentMethodStrings))]
		Transfer
	}
}

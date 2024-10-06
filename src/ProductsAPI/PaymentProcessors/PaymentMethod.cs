using ProductsAPI.Annotations;

namespace ProductsAPI.PaymentProcessors
{
    public enum PaymentMethod
	{
		[PaymentProcessorType(ProcessorType = typeof(CashPaymentProcessor))]
		Cash,
		[PaymentProcessorType(ProcessorType = typeof(TdcPaymentProcessor))]
		Card,
		[PaymentProcessorType(ProcessorType = typeof(TransferPaymentProcessor))]
		Transfer
	}
}

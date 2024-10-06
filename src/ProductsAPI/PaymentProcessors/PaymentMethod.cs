using ProductsAPI.Annotations;

namespace ProductsAPI.PaymentProcessors
{
    public enum PaymentMethod
	{
		[PaymentProcessorType(ProcessorType = typeof(CashPaymentProcessor))]
		Cash,
		[PaymentProcessorType(ProcessorType = typeof(TdcPaymentProcessor))]
		TDC,
		[PaymentProcessorType(ProcessorType = typeof(TransferPaymentProcessor))]
		Transfer
	}
}

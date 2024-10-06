using ProductsAPI.Annotations;

namespace ProductsAPI.PaymentProcessors
{
	public enum PaymentMethod
	{
		[PaymentProcessorType(ProcessorType = typeof(ICashPaymentProcessor))]
		Cash,
		[PaymentProcessorType(ProcessorType = typeof(ITdcPaymentProcessor))]
		TDC,
		[PaymentProcessorType(ProcessorType = typeof(ITransferPaymentProcessor))]
		Transfer
	}
}

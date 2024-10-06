namespace ProductsAPI.PaymentProcessors
{
	public class PaymentProcessException : Exception
	{
		public PaymentProcessException()
			: base("An exception occurs processing the payment.", null)
		{

		}

		public PaymentProcessException(Exception innerException)
            : base("An exception occurs processing the payment.", innerException)
        {
            
        }
    }
}

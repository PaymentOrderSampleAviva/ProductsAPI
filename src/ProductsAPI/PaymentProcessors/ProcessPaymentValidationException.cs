namespace ProductsAPI.PaymentProcessors
{
	public class ProcessPaymentValidationException : Exception
	{
        public ProcessPaymentValidationException(string ErrorMessage, Exception innerException)
            : base(ErrorMessage, innerException)
        {
            
        }
    }
}

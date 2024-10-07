namespace ProductsAPI.PaymentsGateway.Annotations;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PaymentProcessorTypeAttribute : Attribute
{
    public required Type ProcessorType { get; set; }
}

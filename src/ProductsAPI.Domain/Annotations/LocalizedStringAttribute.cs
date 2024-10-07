namespace ProductsAPI.Domain.Annotations;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class LocalizedStringAttribute : Attribute
{
	public required string ResourceKey { get; set; }
    public required Type ResourceType { get; set; }
}

using ProductsAPI.Domain.Annotations;

namespace ProductsAPI.Domain.Entities;

public enum ProductStatus
{
	[LocalizedString(ResourceKey = "NotAvailable", ResourceType = typeof(ResourceStrings))]
	NotAvailable = 0,
	Available = 1
}

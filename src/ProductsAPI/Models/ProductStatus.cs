using ProductsAPI.Annotations;

namespace ProductsAPI.Models;

public enum ProductStatus
{
	[LocalizedString(ResourceKey = "NotAvailable", ResourceType = typeof(ResourceStrings))]
	NotAvailable = 0,
	Available = 1
}

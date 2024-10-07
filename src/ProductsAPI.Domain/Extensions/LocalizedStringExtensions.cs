using ProductsAPI.Domain.Annotations;
using System.Resources;

namespace ProductsAPI.Domain.Extensions
{
	public static class LocalizedStringExtensions
	{
		public static string ToLocalizedString(this LocalizedStringAttribute attribute)
		{
			var resourceManager = new ResourceManager(attribute.ResourceType);
			var value = resourceManager.GetString(attribute.ResourceKey);

			return string.IsNullOrEmpty(value)
				? "Localized value not found"
				: value;
		}
	}
}

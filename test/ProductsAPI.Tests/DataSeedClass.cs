using ProductsAPI.DTOs;

namespace ProductsAPI.Tests;

internal class DataSeedClass
{
	public static IReadOnlyList<ProductResponse> GetProductResponseDataSeed()
	{
		return new List<ProductResponse>
		{
			new ProductResponse{ Id = 1, Name = "DELL Inspiron 5050", Details = "32GB / 512 SSD / i7 2.2Ghz ", StatusId = 1, Status = "Available", UnitPrice = 2350.00 },
			new ProductResponse{ Id = 2, Name = "Lenovo 3450", Details = "16GB / 512 SSD / i5 1.6Ghz ",StatusId = 1, Status = "Available", UnitPrice = 2509.00 },
			new ProductResponse{ Id = 3, Name = "Desk Flexible", Details = "4 Positions ",StatusId = 1, Status = "Available", UnitPrice = 3580.00 },
			new ProductResponse{ Id = 4, Name = "Headset 450p", Details = "Logitech Active Noise Can",StatusId = 0, Status = "Not Available", UnitPrice = 459.00 },
		};
	}
}

using ProductsAPI.Data;

namespace ProductsAPI;

public static class ApplicationConfig
{
	public static async Task DataSeedAsync(this WebApplication application)
	{
		var dbContext = application.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
		await dbContext.Database.EnsureCreatedAsync();
	}
}

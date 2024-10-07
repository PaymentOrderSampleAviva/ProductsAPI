using Microsoft.AspNetCore.Mvc;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductsAppService productsAppService, ILogger<ProductsController> logger) : ControllerBase
{

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[ProducesDefaultResponseType(typeof(List<ProductDto>))]
	public async Task<IActionResult> GetProductsAsync()
	{
		try
		{
			var result = await productsAppService.GetProductsAsync();
			return Ok(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An exception was thrown getting the products list.");
		}

		return StatusCode(500);
	}
}

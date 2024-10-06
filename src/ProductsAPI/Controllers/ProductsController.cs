using Microsoft.AspNetCore.Mvc;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductsAppService productsAppService, ILogger<ProductsController> logger) : ControllerBase
{
	private readonly IProductsAppService _productsAppService = productsAppService;
	private readonly ILogger<ProductsController> _logger = logger;

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[ProducesDefaultResponseType(typeof(List<ProductDto>))]
	public async Task<IActionResult> GetProductsAsync()
	{
		try
		{
			var result = await _productsAppService.ListAllAsync();
			return Ok(result);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An exception was thrown getting the products list.");
		}

		return StatusCode(500);
	}
}

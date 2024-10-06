using Microsoft.AspNetCore.Mvc;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;

namespace ProductsAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController(IOrdersAppService orderAppService, ILogger<OrdersController> logger) : ControllerBase
	{
		private readonly IOrdersAppService _orderAppService = orderAppService;
		private readonly ILogger _logger = logger;

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType(typeof(List<OrderDto>))]
		public async Task<IActionResult> GetOrdersAsync()
		{
			try
			{
				var result = await _orderAppService.ListOrdersAsync();
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception was thrown getting the list or orders.");
			}

			return StatusCode(500);
		}

		[HttpPost("Create")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType(typeof(OrderDto))]
		public async Task<IActionResult> CreateAsync(CreateOrderDto orderDto)
		{
			try
			{
				var result = await _orderAppService.CreateOrderAsync(orderDto);
				return Created(string.Empty, result);
			}
			catch (ArgumentNullException)
			{
				return BadRequest("Validation exception occurs, check your payload.");
			}
			catch (ArgumentException)
			{
				return BadRequest("Validation exception occurs, check your payload.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occurs processing the order.");
			}

			return StatusCode(500);
		}

		[HttpPost("Cancel")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesDefaultResponseType(typeof(OrderDto))]
		public async Task<IActionResult> CancelAsync(CancelOrderDto orderDto)
		{
			try
			{
				var result = await _orderAppService.CancelOrderAsync(orderDto.OrderId, orderDto.Reason ?? string.Empty);
				return Ok(result);
			}
			catch (ArgumentNullException)
			{
				return BadRequest("Validation exception occurs, check your payload.");
			}
			catch (ArgumentException)
			{
				return BadRequest("Validation exception occurs, check your payload.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An exception occurs cancelling the order.");
			}

			return StatusCode(500);
		}

	}
}

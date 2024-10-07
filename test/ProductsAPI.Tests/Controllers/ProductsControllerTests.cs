using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using ProductsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Application.Abstractions;

namespace ProductsAPI.Tests.Controllers;

[TestClass]
public class ProductsControllerTests
{
    private readonly Mock<IProductsApplication> appServiceMock;
    private readonly Mock<ILogger<ProductsController>> loggerMock;

    public ProductsControllerTests()
    {
        appServiceMock = new Mock<IProductsApplication>();
        loggerMock = new Mock<ILogger<ProductsController>>();
    }

    [TestMethod]
	public async Task GetProductsShouldReturnAllItems()
	{
        //arrange
        appServiceMock.Setup(m => m.GetProductsAsync(default)).Returns(() => Task.FromResult(DataSeedClass.GetProductResponseDataSeed()));
        var controller = new ProductsController(appServiceMock.Object, loggerMock.Object);

        // act
        var okResult = await controller.GetProductsAsync() as OkObjectResult;
        var data = okResult?.Value as List<ProductDto>;

		// assert
		okResult.Should().NotBeNull();
        data.Should().NotBeNull();
        data.Should().BeEquivalentTo(DataSeedClass.GetProductResponseDataSeed());
	}
}

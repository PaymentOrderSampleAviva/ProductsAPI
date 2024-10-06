using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using ProductsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.DataContracts;
using ProductsAPI.AppServices.Abstractions;

namespace ProductsAPI.Tests.Controllers;

[TestClass]
public class ProductsControllerTests
{
    private readonly Mock<IProductsAppService> appServiceMock;
    private readonly Mock<ILogger<ProductsController>> loggerMock;

    public ProductsControllerTests()
    {
        appServiceMock = new Mock<IProductsAppService>();
        loggerMock = new Mock<ILogger<ProductsController>>();
    }

    [TestMethod]
	public async Task GetProductsShouldReturnAllItems()
	{
        //arrange
        appServiceMock.Setup(m => m.ListAllAsync()).Returns(() => Task.FromResult(DataSeedClass.GetProductResponseDataSeed()));
        var controller = new ProductsController(appServiceMock.Object, loggerMock.Object);

        // act
        var okResult = await controller.GetProductsAsync() as OkObjectResult;
        var data = okResult?.Value as List<ProductResponse>;

		// assert
		okResult.Should().NotBeNull();
        data.Should().NotBeNull();
        data.Should().BeEquivalentTo(DataSeedClass.GetProductResponseDataSeed());
	}
}

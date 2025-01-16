using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.Application.IServices;
using ProductCatalog.Application.ViewModels;
using ProductCatalog.Controllers;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Test.ControllerLayer
{

    public class ProductControllerTests
    {
        private readonly ProductController _controller;
        private readonly Mock<IProductService> _mockService;
        private readonly Mock<IFileService> _mockFileService;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _mockFileService = new Mock<IFileService>();

            _controller = new ProductController(_mockService.Object, _mockFileService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product" } };
            _mockService.Setup(service => service.GetProductsAsync(false)).ReturnsAsync(products);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var product = new ProductViewModel { Name = "New Product" };

            // Act
            var result = await _controller.Create(product);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockService.Verify(service => service.AddProductAsync(product), Times.Once);
        }

        [Fact]
        public async Task Edit_Get_ExistingId_ReturnsViewWithProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product"
            };
            _mockService
                .Setup(service => service.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal("Test Product", model.Name);
        }

        [Fact]
        public async Task Delete_Get_ExistingId_ReturnsViewWithProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Test Product"
            };
            _mockService
                .Setup(service => service.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Product>(viewResult.Model);
            Assert.Equal("Test Product", model.Name);
        }



    }

}

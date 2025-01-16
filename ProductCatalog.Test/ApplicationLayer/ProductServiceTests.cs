
using Moq;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.ViewModels;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.IRepositories;

namespace ProductCatalog.Test.ApplicationLayer;
public class ProductServiceTests
{
    private readonly ProductService _productService;
    private readonly Mock<IProductRepository> _mockRepository;

    public ProductServiceTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsAllProducts()
    {
        // Arrange
        var products = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1" },
                new Product { Id = 2, Name = "Test Product 2" }
            };

        _mockRepository.Setup(repo => repo.GetProductsAsync(false)).ReturnsAsync(products);

        // Act
        var result = await _productService.GetProductsAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetProductByIdAsync_ExistingId_ReturnsProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product 1" };

        _mockRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

        // Act
        var result = await _productService.GetProductByIdAsync(1);

        // Assert
        Assert.Equal("Test Product 1", result.Name);
    }

    [Fact]
    public async Task AddProductAsync_ValidProduct_AddsProduct()
    {
        // Arrange
        var product = new ProductViewModel { Name = "New Product" };

        // Act
        await _productService.AddProductAsync(product);

        // Assert
        _mockRepository.Verify(repo => repo.AddProductAsync(ProductViewModel.MapToEntity(product)), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ValidProduct_UpdatesProduct()
    {
        // Arrange
        var product = new ProductViewModel
        {
            Id = 1,
            Name = "Updated Product"
        };

        // Act
        await _productService.UpdateProductAsync(product);

        // Assert
        _mockRepository.Verify(
            repo => repo.UpdateProductAsync(ProductViewModel.MapToEntity(product)),
            Times.Once
        );
    }

    [Fact]
    public async Task DeleteProductAsync_ValidId_DeletesProduct()
    {
        // Arrange
        var productId = 1;

        // Act
        await _productService.DeleteProductAsync(productId);

        // Assert
        _mockRepository.Verify(
            repo => repo.DeleteProductAsync(productId),
            Times.Once
        );
    }

}


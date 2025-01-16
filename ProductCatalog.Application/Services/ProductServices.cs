using ProductCatalog.Application.IServices;
using ProductCatalog.Application.ViewModels;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.IRepositories;

namespace ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(bool forAdmin = false)
        {
            return await _productRepository.GetProductsAsync(forAdmin);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(ProductViewModel viewmodel)
        {
            var product = ProductViewModel.MapToEntity(viewmodel);
            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductViewModel viewmodel)
        {
            var product = ProductViewModel.MapToEntity(viewmodel);
            await _productRepository.UpdateProductAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetProductCategoriesAsync()
        {
            return await _productRepository.GetProductCategoriesAsync();
        }
    }

}

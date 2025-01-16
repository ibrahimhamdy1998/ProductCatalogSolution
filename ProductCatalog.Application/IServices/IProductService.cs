using ProductCatalog.Application.ViewModels;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Application.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(bool forAdmin = false);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductViewModel viewModel);
        Task UpdateProductAsync(ProductViewModel viewModel);
        Task DeleteProductAsync(int id);

        Task<IEnumerable<Category>> GetProductCategoriesAsync();
    }


}

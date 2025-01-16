using ProductCatalog.Core.Entities;

namespace ProductCatalog.Infrastructure.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(bool forAdmin = false);
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        Task<IEnumerable<Category>> GetProductCategoriesAsync();

        Task<int> SaveChangesAsync();
    }

}

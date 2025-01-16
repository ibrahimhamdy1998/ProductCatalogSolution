using ProductCatalog.Core.Entities;

namespace ProductCatalog.Infrastructure.IRepositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
    }

}

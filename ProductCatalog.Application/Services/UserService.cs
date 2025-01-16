using ProductCatalog.Application.IServices;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.IRepositories;

namespace ProductCatalog.Application.Services
{
    public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<ApplicationUser> GetUserByIdAsync(string userId)
            {
                return await _userRepository.GetUserByIdAsync(userId);
            }

            public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
            {
                return await _userRepository.GetUsersAsync();
            }

            public async Task AddUserAsync(ApplicationUser user)
            {
                await _userRepository.AddUserAsync(user);
            }

            public async Task UpdateUserAsync(ApplicationUser user)
            {
                await _userRepository.UpdateUserAsync(user);
            }

            public async Task DeleteUserAsync(string userId)
            {
                await _userRepository.DeleteUserAsync(userId);
            }
        }

}

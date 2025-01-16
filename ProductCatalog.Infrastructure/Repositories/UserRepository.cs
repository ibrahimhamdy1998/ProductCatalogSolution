using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.IRepositories;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
        {
            private readonly ApplicationDbContext _context;

            public UserRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ApplicationUser> GetUserByIdAsync(string userId)
            {
                return await _context.ApplicationUsers.FindAsync(userId);
            }

            public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
            {
                return await _context.ApplicationUsers.ToListAsync();
            }

            public async Task AddUserAsync(ApplicationUser user)
            {
                _context.ApplicationUsers.Add(user);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateUserAsync(ApplicationUser user)
            {
                _context.ApplicationUsers.Update(user);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteUserAsync(string userId)
            {
                var user = await _context.ApplicationUsers.FindAsync(userId);
                _context.ApplicationUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

}

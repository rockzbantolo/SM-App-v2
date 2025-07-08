using Microsoft.EntityFrameworkCore;
using ServiceDesk.Core.Models;
using ServiceDesk.Core.Interfaces;
using ServiceDesk.Core.Shared;
using ServiceDesk.Infrastructure.Data;


namespace ServiceDesk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<UserAuth?> GetByCredentialsAsync(string username, string password)
        {
            try
            {
                return await _db.AuthUser.FirstOrDefaultAsync(u =>
                    u.Username == username && u.PasswordHash == password);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to load user data.", ex);
                return null;
            }
        }

        public IQueryable<UserAuth> GetAllUsers()
        {
            return _db.AuthUser.AsQueryable(); // or just: return _db.Users;
        }

    }
}

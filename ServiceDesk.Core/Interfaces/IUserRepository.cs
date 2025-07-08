using ServiceDesk.Core.Models;

namespace ServiceDesk.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserAuth?> GetByCredentialsAsync(string username, string password);

        IQueryable<UserAuth> GetAllUsers();

    }
}

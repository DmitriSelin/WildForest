using WildForest.Domain.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);
    }
}
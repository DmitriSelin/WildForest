using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailWithCityAsync(Email email);

        Task AddUserAsync(User user);

        Task<User?> GetUserByIdAsync(UserId userId);

        Task<User?> GetUserWithCityByIdAsync(UserId userId);
    }
}
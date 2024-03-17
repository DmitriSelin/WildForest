using WildForest.Application.Common.Interfaces.Persistence.Base;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(Email email);

    Task AddUserAsync(User user);

    Task<User?> GetUserByIdAsync(UserId userId);
}
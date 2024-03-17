using WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(Email email);

    Task<User?> GetUserByIdAsync(UserId userId);
}
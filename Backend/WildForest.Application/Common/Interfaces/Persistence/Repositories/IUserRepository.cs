using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailWithCityAsync(Email email);

    Task<User?> GetUserByEmailAsync(Email email);

    Task AddUserAsync(User user);

    Task<User?> GetUserByIdAsync(PersonId userId);

    Task<User?> GetUserWithCityByIdAsync(PersonId userId);
}
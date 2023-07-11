using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IAdminRepository
{
    Task<Admin?> GetAdminByEmailAsync(Email email);

    Task AddAdminAsync(Admin admin);

    Task<Admin?> GetAdminByIdAsync(PersonId adminId);
}
using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(WildForestDbContext context) : base(context) { }

    public async Task<User?> GetUserByEmailAsync(Email email)
    {
        return await Context.Users
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<User?> GetUserByIdAsync(UserId userId)
    {
        return await Context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }
}
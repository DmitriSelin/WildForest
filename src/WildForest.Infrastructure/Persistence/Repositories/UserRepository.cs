using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly WildForestDbContext _context;

    public UserRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> GetUserByEmailAsync(Email email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<User?> GetUserByIdAsync(UserId userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }
}
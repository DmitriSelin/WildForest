using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Clients.Users.Entities;
using WildForest.Domain.Clients.ValueObjects;
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
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(Email email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<User?> GetUserByEmailWithCityAsync(Email email)
    {
        return await _context.Users
            .Include(x => x.City)
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<User?> GetUserByIdAsync(PersonId userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User?> GetUserWithCityByIdAsync(PersonId userId)
    {
        return await _context.Users
            .Include(p => p.City)
            .FirstOrDefaultAsync(p => p.Id == userId);
    }
}
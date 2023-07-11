using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Clients.Admins.Entites;
using WildForest.Domain.Clients.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class AdminRepository : IAdminRepository
{
    private readonly WildForestDbContext _context;

    public AdminRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddAdminAsync(Admin admin)
    {
        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync();
    }

    public async Task<Admin?> GetAdminByEmailAsync(Email email)
    {
        return await _context.Admins
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value);
    }

    public async Task<Admin?> GetAdminByIdAsync(PersonId adminId)
    {
        return await _context.Admins
            .FirstOrDefaultAsync(x => x.Id == adminId);
    }
}
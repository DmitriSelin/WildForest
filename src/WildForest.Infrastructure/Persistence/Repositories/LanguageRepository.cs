using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Languages.Entities;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class LanguageRepository : ILanguageRepository
{
    private readonly WildForestDbContext _context;

    public LanguageRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        => await _context.Languages.ToListAsync();
}
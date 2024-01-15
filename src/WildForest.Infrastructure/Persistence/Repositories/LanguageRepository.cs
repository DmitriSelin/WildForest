using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;
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

    public async Task<Language?> GetLanguageByIdAsync(LanguageId languageId)
        => await _context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
}
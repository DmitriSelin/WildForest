using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class LanguageRepository : Repository<Language>, ILanguageRepository
{
    public LanguageRepository(WildForestDbContext context) : base(context) { }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        => await Context.Languages.ToListAsync();

    public async Task<Language?> GetLanguageByIdAsync(LanguageId languageId)
        => await Context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
}
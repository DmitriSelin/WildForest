using WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;
using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface ILanguageRepository : IRepository<Language>
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();

    Task<Language?> GetLanguageByIdAsync(LanguageId languageId);
}
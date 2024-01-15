using WildForest.Domain.Languages.Entities;
using WildForest.Domain.Languages.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();

    Task<Language?> GetLanguageByIdAsync(LanguageId languageId);
}
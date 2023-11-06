using WildForest.Domain.Languages.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
}
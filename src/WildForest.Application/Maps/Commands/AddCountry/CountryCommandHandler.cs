using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Maps.Commands.AddCountry;

public sealed class CountryCommandHandler : ICountryCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<string>> AddCountryAsync(CountryCommand command)
    {
        var countryName = CountryName.Create(command.CountryName);

        var country = await _unitOfWork.CountryRepository.GetCountryByNameAsync(countryName);

        if (country is not null)
        {
            return Errors.Country.Duplicate;
        }

        country = Country.Create(countryName);

        await _unitOfWork.CountryRepository.AddCountryAsync(country);
        await _unitOfWork.SaveChangesAsync();

        var result = "The country was successfully added";
        return result;
    }
}
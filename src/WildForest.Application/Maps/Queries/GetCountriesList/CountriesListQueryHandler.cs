using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Maps.Queries.GetCountriesList;

public sealed class CountriesListQueryHandler : ICountriesListQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CountriesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CountryQuery>> GetCountriesAsync()
    {
        var countries = (List<Country>)await _unitOfWork.CountryRepository.GetAllCountriesAsync();

        return _mapper.Map<List<CountryQuery>>(countries);
    }
}
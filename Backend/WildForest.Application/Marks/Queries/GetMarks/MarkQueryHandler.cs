using WildForest.Domain.Common.Errors;
using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Marks.Common;
using WildForest.Domain.Weather.ValueObjects;
using MapsterMapper;

namespace WildForest.Application.Marks.Queries.GetMarks;

public sealed class MarkQueryHandler : IMarkQueryHandler
{
    private readonly IMarkRepository _markRepository;
    private readonly IMapper _mapper;

    public MarkQueryHandler(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<MarkDto>>> GetMarksByWeatherForecastAsync(Guid weatherId)
    {
        var id = WeatherId.Create(weatherId);

        var marks = await _markRepository.GetMarksByWeatherIdAsync(id);

        if (marks is null)
            return Errors.Mark.NotFound;

        var marksDto = _mapper.Map<List<MarkDto>>(marks);

        return marksDto;
    }
}
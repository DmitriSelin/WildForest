using ErrorOr;
using WildForest.Domain.Common.Errors;
using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Application.Marks.Common;

namespace WildForest.Application.Marks.Queries.GetComments;

public sealed class CommentsQueryHandler : ICommentsQueryHandler
{
    private readonly IMarkRepository _markRepository;
    private readonly IMapper _mapper;

    public CommentsQueryHandler(IMarkRepository markRepository, IMapper mapper)
    {
        _markRepository = markRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<CommentsModel>>> GetCommentsAsync(Guid weatherId)
    {
        var id = WeatherId.Create(weatherId);

        var marks = await _markRepository.GetMarksWithCommentsByWeatherIdAsync(id);

        if (marks is null)
            return Errors.Mark.NotFound;

        var markList = _mapper.Map<List<CommentsModel>>(marks);
        return markList;
    }
}
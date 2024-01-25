namespace WildForest.Application.Common.Models;

public abstract class Dto
{
    public Guid Id { get; init; }

    protected Dto(Guid id)
        => Id = id;
}
namespace WildForest.Dto.Models;

public abstract class BaseDto
{
    public Guid Id { get; init; }

    protected BaseDto(Guid id)
        => Id = id;
}
namespace WildForest.Dto.Models;

public class NamedDto : BaseDto
{
    public string Name { get; init; }

    public NamedDto(Guid id, string name) : base(id)
        => Name = name;
}
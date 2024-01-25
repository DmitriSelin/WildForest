namespace WildForest.Application.Common.Models;

public class NamedDto : Dto
{
    public string Name { get; init; }

    public NamedDto(Guid id, string name) : base(id)
        => Name = name;
}
namespace WildForest.Application.Common.Models;

public class NamedDto
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public NamedDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
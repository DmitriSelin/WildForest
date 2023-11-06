using WildForest.Domain.Common.Models;
using WildForest.Domain.Languages.ValueObjects;
using WildForest.Domain.Users.Entities;

namespace WildForest.Domain.Languages.Entities;

public sealed class Language : Entity<LanguageId>
{
    public string Name { get; } = null!;

    public static Language Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), "Nullable language name");

        name = name.Trim();

        return new(LanguageId.Create(), name);
    }

    private readonly List<User> _users = new();

    public IReadOnlyList<User> Users => _users.AsReadOnly();

    private Language(LanguageId id, string name) : base(id)
        => Name = name;

#pragma warning disable IDE0051 // Remove unused private members
    private Language(LanguageId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Common.Models
{
    public class Person<TId> : Entity<TId>
        where TId : notnull
    {
        public FirstName FirstName { get; } = null!;

        public LastName LastName { get; } = null!;

        public Role Role { get; }

        public Email Email { get; } = null!;

        public Password Password { get; } = null!;

        private readonly List<RefreshToken> _refreshTokens = new();

        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        protected Person(
            TId id,
            FirstName firstName,
            LastName lastName,
            Role role,
            Email email,
            Password password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Email = email;
            Password = password;
        }

        private Person(TId id) : base(id) { }
    }
}
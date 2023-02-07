using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities
{
    public class User : Entity<UserId>
    {
        public FirstName FirstName { get; } = null!;

        public LastName LastName { get; } = null!;

        public Role Role { get; }

        public Email Email { get; } = null!;

        public Password Password { get; } = null!;

        public CityId CityId { get; } = null!;

        public virtual City City { get; } = null!;

        private User(
            UserId id,
            FirstName firstName,
            LastName lastName,
            Email email,
            Password password,
            CityId cityId) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = Role.User;
            Email = email;
            Password = password;
            CityId = cityId;
        }

        private User(UserId id) : base(id) { }

        public static User Create(
            FirstName firstName,
            LastName lastName, 
            Email email, 
            Password password,
            CityId cityId)
        {
            return new(
                UserId.Create(), 
                firstName,
                lastName, 
                email, 
                password,
                cityId);
        }
    }
}
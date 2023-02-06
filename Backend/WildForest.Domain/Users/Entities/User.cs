using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities
{
    public class User : Entity<UserId>
    {
        public FirstName FirstName { get; }

        public LastName LastName { get; }

        public Role Role { get; }

        public Email Email { get; }

        public Password Password { get; }

        public CityId CityId { get; }

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

        public static User CreateUser(
            FirstName firstName,
            LastName lastName, 
            Email email, 
            Password password,
            CityId cityId)
        {
            return new(
                UserId.CreateUserId(), 
                firstName,
                lastName, 
                email, 
                password,
                cityId);
        }
    }
}
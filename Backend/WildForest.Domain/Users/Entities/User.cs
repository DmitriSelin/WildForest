using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities
{
    public class User : Entity<UserId>
    {
        public string FirstName { get; }

        public string LastName { get; }

        public Role Role { get; }

        public string Email { get; }

        public string Password { get; }

        public CityId CityId { get; } = null!;

        public virtual City City { get; } = null!;

        private User(
            UserId id,
            string firstName,
            string lastName,
            Role role,
            string email,
            string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Email = email;
            Password = password;
        }

        public static User CreateUser(
            string firstName, 
            string lastName, 
            Role role, 
            string email, 
            string password)
        {
            return new(
                UserId.CreateUserId(), 
                firstName,
                lastName, 
                role,
                email, 
                password);
        }
    }
}
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Users.Entities
{
    public class User : Entity<UserId>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Mark> Marks { get; set; } = null!;

        public CityId CityId { get; } = null!;

        public virtual City City { get; } = null!;

        public User(
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
    }
}
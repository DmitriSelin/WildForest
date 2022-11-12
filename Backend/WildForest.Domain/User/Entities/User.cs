using WildForest.Domain.City.ValueObjects;
using WildForest.Domain.Common.Enums;
using WildForest.Domain.Common.Models;
using WildForest.Domain.User.ValueObjects;

namespace WildForest.Domain.User.Entities
{
    public class User : Entity<UserId>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Mark.Entities.Mark> Marks { get; set; } = null!;

        public CityId CityId { get; } = null!;

        public virtual City.Entities.City City { get; } = null!;

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
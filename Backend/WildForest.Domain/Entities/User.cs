using WildForest.Domain.Common.Enums;
using WildForest.Domain.Models;

namespace WildForest.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User(
            Guid id,
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
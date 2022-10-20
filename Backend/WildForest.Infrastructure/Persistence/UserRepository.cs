using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Entities;

namespace WildForest.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        public static List<User> Users = new List<User>();

        public async Task AddUserAsync(User user)
        {
            Users.Add(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
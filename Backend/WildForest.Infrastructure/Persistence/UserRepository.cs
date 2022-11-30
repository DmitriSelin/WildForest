using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Persistence
{
    public sealed class UserRepository : IUserRepository
    {
        private static List<User> Users = new();

        private void AddUser(User user)
        {
            Users.Add(user);
        }

        public async Task AddUserAsync(User user)
        {
            await Task.Run(() => AddUser(user));
        }

        private User? GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(x => x.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await Task.Run(() => GetUserByEmail(email));
        }

        private User? GetUserById(UserId userId)
        {
            return Users.FirstOrDefault(x => x.Id == userId);
        }

        public async Task<User?> GetUserByIdAsync(UserId userId)
        {
            return await Task.Run(() => GetUserById(userId));
        }
    }
}
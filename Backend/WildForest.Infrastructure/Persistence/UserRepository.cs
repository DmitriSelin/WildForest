﻿using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Users.Entities;

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
    }
}
using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkingPlatform.Repositories
{
    /// <summary>
    /// Хэрэглэгчийн санах ойн Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }
        private readonly List<IUser> _users = new();

        public void Add(IUser user) => _users.Add(user);

        public void Remove(IUser user) => _users.Remove(user);

        public IUser? GetById(Guid id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public IUser? GetByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email == email);

        public IUser? GetByUsername(string username) =>
            _users.FirstOrDefault(u => u.Username == username);

        public void Update(IUser user)
        {
            var idx = _users.FindIndex(u => u.Id == user.Id);
            if (idx == -1)
                throw new KeyNotFoundException($"ID:{user.Id} олдсонгүй.");
            _users[idx] = user;
        }

        public IReadOnlyList<IUser> GetAll() => _users.AsReadOnly();
    }
}

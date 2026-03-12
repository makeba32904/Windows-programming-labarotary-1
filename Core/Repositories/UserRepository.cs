using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }
        public User? GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public List<User> GetAll()
        {
            return _users;
        }
    }
}

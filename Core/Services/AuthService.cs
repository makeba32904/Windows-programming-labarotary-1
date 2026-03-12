using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User? Register(string username, string password)
        {
            if (_userRepo.GetByUsername(username) != null)
                return null;
            var user = new User
            {
                Username = username,
                Password = password
            };
            _userRepo.Add(user);
            return user;
        }

        public User? Login(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);

            if (user != null && user.Password == password)
                return user;

            return null;
        }
    }
}

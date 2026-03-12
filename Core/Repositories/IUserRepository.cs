using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByUsername(string username);
        List<User> GetAll();
    }
}

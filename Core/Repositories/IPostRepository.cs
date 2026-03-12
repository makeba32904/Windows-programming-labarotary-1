using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        Post? GetById(Guid id);
        List<Post> GetAll();
        List<Post> GetByUser(Guid userId);
    }
}

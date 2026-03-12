using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
namespace Core.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new();
        public void Add(Post post)
        {
            _posts.Add(post);
        }
        public Post? GetById(Guid id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }
        public List<Post> GetAll()
        {
            return _posts;
        }
        public List<Post> GetByUser(Guid userId)
        {
            return _posts.Where(p => p.AuthorId == userId).ToList();
        }
    }
}

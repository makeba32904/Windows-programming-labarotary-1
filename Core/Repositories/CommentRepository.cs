using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly List<Comment> _comments = new();
        public void Add(Comment comment)
        {
            _comments.Add(comment);
        }
        public List<Comment> GetByPost(Guid postId)
        {
            return _comments.Where(c => c.PostId == postId).ToList();
        }
        public List<Comment> GetAll()
        {
            return _comments;
        }
    }
}

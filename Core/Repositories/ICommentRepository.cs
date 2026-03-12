using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace Core.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        List<Comment> GetByPost(Guid postId);
        List<Comment> GetAll();
    }
}

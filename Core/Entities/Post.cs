using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Core.Interfaces;
namespace Core.Entities
{
    public class Post : BaseEntity, ILiking, ICommenting
    {
        public Guid AuthorId { get; set; }
        public string Content { get; set; } = "";
        public List<Guid> Likes { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public void Like(Guid userId)
        {
            Likes.Add(userId);
        }
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}

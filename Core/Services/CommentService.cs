using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }
        public void AddComment(Guid postId, Guid userId, string text)
        {
            var post = _postRepository.GetById(postId)
            if (post == null)
            {
                Console.WriteLine("Post not found.");
                return;
            }

            var comment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Text = text
            };
            _commentRepository.Add(comment);
            post.AddComment(comment);

            Console.WriteLine("Comment added.");
        }
        public List<Comment> GetComments(Guid postId)
        {
            return _commentRepository.GetByPost(postId);
        }
    }
}

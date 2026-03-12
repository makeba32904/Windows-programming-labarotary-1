using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        public void CreatePost(Guid userId, string content)
        {
            var post = new Post
            {
                AuthorId = userId,
                Content = content
            };
            _postRepository.Add(post);
        }
        public List<Post> GetFeed()
        {
            return _postRepository.GetAll();
        }
        public User? GetUserById(Guid id)
        {
            return _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == id);
        }
        public void LikePost(Guid postId, Guid userId)
        {
            var post = _postRepository.GetById(postId);
            if (post == null)
                return;
            post.Like(userId);
        }
    }
}

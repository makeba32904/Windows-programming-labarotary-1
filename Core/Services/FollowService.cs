using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class FollowService
    {
        private readonly IUserRepository _userRepository;
        public FollowService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Follow(Guid followerId, Guid followingId)
        {
            var users = _userRepository.GetAll();
            var follower = users.FirstOrDefault(u => u.Id == followerId);
            var following = users.FirstOrDefault(u => u.Id == followingId);
            if (follower == null || following == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            follower.Follow(followingId);
            following.Followers.Add(followerId);
            Console.WriteLine($"{follower.Username} followed {following.Username}");
        }
    }
}

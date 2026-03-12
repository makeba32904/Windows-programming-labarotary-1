using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : BaseEntity, IFollowable
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Bio { get; set; } = "";
        public List<Guid> Followers { get; set; } = new();
        public List<Guid> Following { get; set; } = new();
        public void Follow(Guid userId)
        {
            Following.Add(userId);
        }
    }
}
